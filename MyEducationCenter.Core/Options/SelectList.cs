namespace MyEducationCenter.Core;

public class SelectList<T>
{
    public required virtual T Value {  get; set; }
    public required  virtual string Text { get; set; }

}

public class SelectList: SelectList<long>
{

}
