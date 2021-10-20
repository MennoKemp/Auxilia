using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Auxilia.Presentation.Behaviors
{
	public class UpdateOnEnterBehavior : Behavior<TextBox>
	{
		protected override void OnAttached()
		{
			AssociatedObject.KeyDown += OnKeyDown;
		}

		protected override void OnDetaching()
		{
			AssociatedObject.KeyDown -= OnKeyDown;
		}

		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Enter)
				BindingOperations.GetBindingExpression(AssociatedObject, TextBox.TextProperty)?.UpdateSource();
		}
	}
}
