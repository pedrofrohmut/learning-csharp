// dotnet run -v quiet

namespace ns20;

interface IEletronicDevice
{
    void On();
    void Off();
    void VolumeUp();
    void VolumeDown();
}

class Television : IEletronicDevice
{
    public int Volume { get; set; }
    public bool IsOn { get; set; }

    public void Off() { IsOn = false; }

    public void On() { IsOn = true; }

    public void VolumeDown() { if (Volume > 0) Volume--; }

    public void VolumeUp() { if (Volume < 100) Volume++; }
}

class TVRemote
{
    public static IEletronicDevice GetDevice() => new Television();
}

interface ICommand
{
    void Execute();
    void Undo();
}

class PowerButton : ICommand
{
    IEletronicDevice device;

    public PowerButton(IEletronicDevice device) { this.device = device; }

    public void Execute() { device.Off(); }

    public void Undo() { device.On(); }
}


class ComplexClassExample
{
    static void __Main(string[] args)
    {
        var tv = TVRemote.GetDevice();
        var btn = new PowerButton(tv);
        btn.Execute();
        btn.Undo();
    }
}
