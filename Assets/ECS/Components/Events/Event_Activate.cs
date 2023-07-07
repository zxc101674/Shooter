using System;

[Serializable]
public struct Event_Activate : IActivate {
    public SO_Dialog Dialog { get; set; }
    public Action OnActivate { get; set; }
}
public class Mono_Event_Activate : BaseMonoComponent<Event_Activate> { }
