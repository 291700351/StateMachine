using Godot;
using System;
using System.Data.Common;
using System.Linq;

namespace Lee.FSM;
/// <summary>
/// 状态机
/// </summary>
[Tool]
public partial class StateMachine : Node
{

	[Export] bool IsDebug = false;
	[Signal] public delegate void OnReadyEventHandler(State state);
	[Signal] public delegate void OnEnterEventHandler(State state);
	[Signal] public delegate void OnExitEventHandler(State state);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="os"> Old State</param>
	/// <param name="ns">New State</param>
	[Signal] public delegate void OnStateChangedEventHandler(State os, State ns);
	private State currentState = new ResetState();

	public override void _Ready()
	{
		Godot.Collections.Array<Node> children = GetChildren();
		if (null == children || children.Count == 0)
		{
			Debug("Do not have any state node, Skip it.");
			return;
		}
		foreach (Node item in children)
		{
			if (item is not State)
			{
				Debug($"'{item.Name}' is not a state node, Please check it.");
				continue;
			}
			State s = item as State;
			s.OnReady();
			EmitSignal(nameof(OnReady), currentState);
			s.changeState += ChangeState;

		}
		//默认初始化第一个为当前状态
		foreach (var node in children)
		{
			if (node is State)
			{
				State s = node as State;
				SwitchState(s);
				break;
			}
		}
	}

	public override void _UnhandledKeyInput(InputEvent @event) { currentState?.UnhandledKeyInput(@event); }

	public override void _Process(double delta) { currentState?.OnProcess(delta); }

	public override void _PhysicsProcess(double delta) { currentState?.OnPhysics(delta); }
	private void SwitchState(State state)
	{
		if (null == state)
		{
			throw new NullReferenceException("You want to switch to a null state");
		}
		if(state.Equals(currentState)){
			Debug($"Target state is equals current, Skip it.");
			return;
		}
		if (null != currentState)
		{
			currentState.OnExit();
			EmitSignal(nameof(OnExit), currentState);
		}
		state.OnEnter();
		EmitSignal(nameof(OnEnter), state);
  
		EmitSignal(nameof(OnStateChanged), currentState, state);
		Debug($"{currentState.Name} => {state.Name}");
		currentState = state; 
	}

	private void ChangeState(String name)
	{
		if (!HasNode(name))
		{
			Debug($"Can not find state named '{name}'");
			return;
		}
		Node node = GetNode(name);
		if (null == node)
		{
			Debug("Target state node is null,Check it.");
			return;
		}
		if (node is not State)
		{
			Debug($"Target state '{name}' is not a State node");
			return;
		}
		if (node.Equals(currentState))
		{
			Debug($"Target state '{name}' equals current, Skip it.");
			return;
		}
		State s = node as State;
		SwitchState(s);

	}



	private void Debug(String text)
	{
		if (!IsDebug) { return; }
		GD.Print($"[{Name}\t{Godot.Engine.GetPhysicsFrames()}]\t{text}");
	}

}
