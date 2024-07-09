using Avalonia;
using Avalonia.Animation;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Launcher.MiniUpdater
{
    public class CyclePageSlide : PageSlide
    {
        public override async Task Start(Visual? from, Visual? to, bool forward, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            var tasks = new List<Task>();
            var parent = GetVisualParent(from, to);
            var distance = Orientation == SlideAxis.Horizontal ? parent.Bounds.Width : parent.Bounds.Height;
            var translateProperty = Orientation == SlideAxis.Horizontal ? TranslateTransform.XProperty : TranslateTransform.YProperty;

            if (from != null)
            {
                var animation = new Animation
                {
                    Easing = SlideOutEasing,
                    Children =
                    {
                        new KeyFrame
                        {
                            Setters = { new Setter { Property = translateProperty, Value = 0d } },
                            Cue = new Cue(0d)
                        },
                        new KeyFrame
                        {
                            Setters =
                            {
                                new Setter
                                {
                                    Property = translateProperty,
                                    Value = -distance
                                }
                            },
                            Cue = new Cue(1d)
                        }
                    },
                    Duration = Duration
                };
                tasks.Add(animation.RunAsync(from, cancellationToken));
            }

            if (to != null)
            {
                to.IsVisible = true;
                var animation = new Animation
                {
                    Easing = SlideInEasing,
                    Children =
                    {
                        new KeyFrame
                        {
                            Setters =
                            {
                                new Setter
                                {
                                    Property = translateProperty,
                                    Value = distance 
                                }
                            },
                            Cue = new Cue(0d)
                        },
                        new KeyFrame
                        {
                            Setters = { new Setter { Property = translateProperty, Value = 0d } },
                            Cue = new Cue(1d)
                        }
                    },
                    Duration = Duration
                };
                tasks.Add(animation.RunAsync(to, cancellationToken));
            }

            await Task.WhenAll(tasks);

            if (from != null && !cancellationToken.IsCancellationRequested)
            {
                from.IsVisible = false;
            }
        }
    }
}
