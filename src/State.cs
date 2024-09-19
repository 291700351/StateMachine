using Godot;
using System;

namespace Lee.FSM;


public partial class State : Node
{
	[Signal] public delegate void changeStateEventHandler(String name);

#pragma warning disable CA1822 // 将成员标记为 static
	public void OnReady() { }
#pragma warning restore CA1822 // 将成员标记为 static
#pragma warning disable CA1822 // 将成员标记为 static
	public void OnEnter() { }

#pragma warning restore CA1822 // 将成员标记为 static
#pragma warning disable CA1822 // 将成员标记为 static
	public void UnhandledKeyInput(InputEvent @event) { }

#pragma warning restore CA1822 // 将成员标记为 static
#pragma warning disable CA1822 // 将成员标记为 static
	public void OnProcess(double delta) { }

#pragma warning restore CA1822 // 将成员标记为 static
#pragma warning disable CA1822 // 将成员标记为 static
	public void OnPhysics(double delta) { }

#pragma warning restore CA1822 // 将成员标记为 static
#pragma warning disable CA1822 // 将成员标记为 static
	public void OnExit() { }

#pragma warning restore CA1822 // 将成员标记为 static

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

	protected void SwitchState(String name)
	{
		EmitSignal(nameof(changeState), name);

	}

}

