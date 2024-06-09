using Avalonia;
using Avalonia.Data;
using Avalonia.Markup.Xaml;

namespace MarkupExtentions;

public class AncestorBindingExtension : MarkupExtension
{
    public Type AncestorType { get; set; }
    public string Path { get; set; }

    public AncestorBindingExtension() { }

    public AncestorBindingExtension(Type ancestorType, string path)
    {
        AncestorType = ancestorType;
        Path = path;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget provideValueTarget &&
            provideValueTarget.TargetObject is AvaloniaObject targetObject)
        {
            var binding = new Binding
            {
                RelativeSource = new RelativeSource
                {
                    Mode = RelativeSourceMode.FindAncestor,
                    AncestorType = AncestorType
                },
                Path = Path
            };

            return binding.ProvideValue(serviceProvider);
        }

        throw new InvalidOperationException("AncestorBindingExtension can only be used with Avalonia objects.");
    }
}