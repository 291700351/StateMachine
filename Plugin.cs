#if TOOLS
using Godot;
using System;
[Tool]
public partial class Plugin : EditorPlugin
{
	private String machineName = "StateMachine";
	private Script machineScript = GD.Load<Script>("addons/StateMachine/src/StateMachine.cs");
	private Texture2D machineIcon = GD.Load<Texture2D>("addons/StateMachine/icons/icon_state_machine.png");

	private String stateName = "State";
	private Script stateScript = GD.Load<Script>("addons/StateMachine/src/State.cs");
	private Texture2D stateIcon = GD.Load<Texture2D>("addons/StateMachine/icons/icon_state_machine.png");


	public override void _EnterTree()
	{
		AddCustomType(machineName, "Node", machineScript, machineIcon);
		AddCustomType(stateName, "Node", stateScript, stateIcon);
	}

	public override void _ExitTree()
	{
		RemoveCustomType(machineName);
		RemoveCustomType(stateName);
	}
}
#endif
