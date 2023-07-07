using Voody.UniLeo;

public class BaseMonoComponent<T> : MonoProvider<T> where T : struct {
    public T Value {
        get => value;
        set => base.value = value;
    }
}
