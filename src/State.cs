using Godot;
using System;

namespace Lee.FSM;

/// <summary>
/// 状态对象
/// </summary>
[Tool]
public partial class State : Node
{
	[Signal] public delegate void changeStateEventHandler(String name);

	public virtual void OnReady() { }
	public virtual void OnEnter() { }
	public virtual void UnhandledKeyInput(InputEvent @event) { }
	public virtual void OnProcess(double delta) { }
	public virtual void OnPhysics(double delta) { }
	public virtual void OnExit() { }


	/// <summary>
	/// 获取Owner，并且强制类型转换，不要在生命周期调用，因为子节点初始化在父节点之前
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	protected T Host<T>() where T : Node
	{
		// Owner.IsNodeReady
		return Owner as T;
	}

	/// <summary>
	/// 请求切换状态
	/// </summary>
	/// <param name="name">需要切换状态名称</param>
	protected void SwitchState(String name)
	{
		EmitSignal(nameof(changeState), name); 
	}

}

