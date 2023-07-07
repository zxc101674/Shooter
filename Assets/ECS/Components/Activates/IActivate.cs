using System;

public interface IActivate {
    public Action OnActivate { get; set; }
    public SO_Dialog Dialog { get; set; }
}
