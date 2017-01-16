using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Interactivity;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace Rhythms.Shared.Xaml.Behaviors
{
	public class SaveChartBehavior : Behavior<Window>
	{
		public Guid Trigger
		{
			get { return (Guid)GetValue(TriggerProperty); }
			set { SetValue(TriggerProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Trigger.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty TriggerProperty =
			DependencyProperty.Register("Trigger", typeof(Guid), typeof(SaveChartBehavior), new PropertyMetadata(default(Guid), OnGuidChanged));

		private static void OnGuidChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var b = d as SaveChartBehavior;
			if (b != null)
			{
				Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
				dlg.FileName = "chart";
				dlg.DefaultExt = ".jpg";
				dlg.Filter = "JPEG Images (.jpg)|*.jpeg";

				var result = dlg.ShowDialog();

				if (result == true)
				{
					var renderBitmap = b.RenderVisual(b.AssociatedObject);

					var imageEencodercoder = new PngBitmapEncoder();

					imageEencodercoder.Frames.Add(BitmapFrame.Create(renderBitmap));

					using (FileStream file = File.Create(dlg.FileName))
					{
						imageEencodercoder.Save(file);
					}
				}
			}
		}

		private RenderTargetBitmap RenderVisual(UIElement elt)
		{
			var source = PresentationSource.FromVisual(elt);
			var rtb = new RenderTargetBitmap((int)elt.RenderSize.Width, (int)elt.RenderSize.Height, 96, 96, PixelFormats.Default);

			var sourceBrush = new VisualBrush(elt);
			var drawingVisual = new DrawingVisual();
			var drawingContext = drawingVisual.RenderOpen();

			using (drawingContext)
			{
				drawingContext.DrawRectangle(sourceBrush, null, new Rect(new System.Windows.Point(0, 0),
					  new System.Windows.Point(elt.RenderSize.Width, elt.RenderSize.Height)));
			}

			rtb.Render(drawingVisual);

			return rtb;
		}
	}
}
