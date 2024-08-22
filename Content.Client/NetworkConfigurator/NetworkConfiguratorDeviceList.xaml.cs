using System.Numerics;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.Controls;

namespace Content.Client.NetworkConfigurator;

[GenerateTypedNameReferences]
public sealed partial class NetworkConfiguratorDeviceList : ScrollContainer
{
    public event Action<string>? OnRemoveAddress;

    public void UpdateState(HashSet<(string address, string name)> devices, bool ui)
    {
        DeviceList.RemoveAllChildren();

        foreach (var device in devices)
        {
            DeviceList.AddChild(BuildDeviceListRow(device, ui));
        }
    }

    private BoxContainer BuildDeviceListRow((string address, string name) savedDevice, bool ui)
    {
        var row = new BoxContainer()
        {
            Orientation = BoxContainer.LayoutOrientation.Horizontal,
            Margin = new Thickness(8)
        };

        var name = new Label()
        {
            Text = savedDevice.name[..Math.Min(11, savedDevice.name.Length)],
            SetWidth = 84
        };

        var address = new Label()
        {
            Text = savedDevice.address,
            HorizontalExpand = true,
            Align = Label.AlignMode.Center
        };

        var removeButton = new TextureButton()
        {
            StyleClasses = { "CrossButtonRed" },
            VerticalAlignment = VAlignment.Center,
            Scale = new Vector2(0.5f, 0.5f)
        };

        row.AddChild(name);
        row.AddChild(address);

        if (ui)
        {
            row.AddChild(removeButton);
            removeButton.OnPressed += _ => OnRemoveAddress?.Invoke(savedDevice.address);
        }

        return row;
    }
}