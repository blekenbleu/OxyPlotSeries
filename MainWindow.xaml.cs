using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace OxyPlotSeries
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void lineSeries_Click(object sender, RoutedEventArgs e)
        {
            double[] x = Enumerable.Range(1, 100).Select(v => ((double)v) / 10).ToArray();
            double[] y = x.Select(v => v * v).ToArray();

            var model = new PlotModel { Title = "OxyPlot - Line Series" };
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Y",
            });
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Number of items",
            });
            model = addLine(model, x, y, 2, OxyColors.Gray);
            model.Series[model.Series.Count - 1].Title = "X2";
            model.LegendPosition = LegendPosition.TopLeft;
            model.LegendFontSize = 12;
            plot.Model = model;

        }
        private void sccaterSeries_Click(object sender, RoutedEventArgs e)
        {
            Random rnd=new Random();
            double[] x = { 1.2, 2.1, 2.9, 4.5, 5, 5.7, 6.9, 8.3, 9.12, 10.0 };
            double[] y = new double[x.Length].Select(v => 10 * rnd.NextDouble()).ToArray();
            var model = new PlotModel { Title = "OxyPlot - Scatter Series" };
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Y",
            });
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Number of items",
            });
            model = addScater(model, x, y, 5, OxyColors.Red);
            model.Series[model.Series.Count - 1].Title = "X2 Data";
/*
            model.Legends.Add(new OxyPlot.Legends.Legend()
            {
                LegendPosition = LegendPosition.TopLeft,
                LegendFontSize = 12
            });
 */
            plot.Model = model;
        }
        private void barSeries_Click(object sender, RoutedEventArgs e)
        {
            double[] data = { 1, 4, 9, 16, 25, 36, 49, 64, 81, 100 };
            var model = new PlotModel { Title = "OxyPlot - Bar Series" };
            model.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Left,
                Title = "Y",
            });


            model = addBar(model, data, OxyColors.DarkGreen);
            model.Series[model.Series.Count - 1].Title = "X2";
/*
            model.Legends.Add(new OxyPlot.Legends.Legend()
            {
                LegendPosition = LegendPosition.BottomRight,
                LegendFontSize = 12
            });
 */
            plot.Model = model;
        }
        private void columnSeries_Click(object sender, RoutedEventArgs e)
        {
            double[][] x = new double[2][];
            x[0] = new double[] { 1.2, 2.1, 2.9, 4.5, 5, 5.7, 6.9, 8.3, 9.12, 10.0 };
            x[1] = new double[] { 1, 1.8, 3.3, 4.0, 5.7, 6.3, 7.6, 7.3, 8.9, 10.6 };
            double[][] y = new double[2][];
            y[0] = new double[] { 1.8, 3.8, 9.6, 15.1, 25.6, 38, 47, 60, 84, 102 };
            y[1] = new double[] { -1.9, -3.8, -9.6, -15.1, -25.6, -38, -47, -60, -84, -102 };
            var model = new PlotModel { Title = "OxyPlot - Column Series" };
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                ExtraGridlines = new Double[] { 0 },
                Title = "Y",
            });
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Number of items",
            });
            var cl = new OxyColor[] { OxyColors.DarkGreen, OxyColors.DarkRed };

            model = addColumn(model, x, y, 15, cl);
            model.Series[model.Series.Count - 2].Title = "X2";
            model.Series[model.Series.Count - 1].Title = "-X2";
/*
            model.Legends.Add(new OxyPlot.Legends.Legend()
            {
                LegendPosition = LegendPosition.BottomRight,
                LegendFontSize = 12
            });
 */
            plot.Model = model;
        }




        #region Methods
        private PlotModel addScater(PlotModel model, double[] x, double[] y, int size, OxyColor color)
        {
            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };
            for (int i = 0; i < x.Length; i++)
            {
                scatterSeries.Points.Add(new ScatterPoint(x[i], y[i], size));
            }
            scatterSeries.MarkerFill = color;
            model.Series.Add(scatterSeries);
            return model;
        }
        private PlotModel addLine(PlotModel model, double[] x, double[] y, double lineThick, OxyColor colorValue)
        {
            var lineSeries = new LineSeries { StrokeThickness = lineThick, Color = colorValue };
            for (int i = 0; i < x.Length; i++)
            {
                lineSeries.Points.Add(new DataPoint(x[i], y[i]));
            }
            model.Series.Add(lineSeries);
            return model;
        }
        private PlotModel addBar(PlotModel model, double[] y, OxyColor color, string tit = "")
        {
            OxyPlot.Series.BarSeries ser = new BarSeries();
            ser.Title = tit;
            ser.FillColor = color;
            ser.LabelPlacement = LabelPlacement.Inside;
            var dd = new List<BarItem>();
            foreach (double yy in y)
                dd.Add(new BarItem { Value = yy });
            ser.ItemsSource = dd;
            model.Series.Add(ser);
            return model;

        }
        private PlotModel addColumn(PlotModel model, double[][] x, double[][] y, double barWidth, OxyColor[] colorValue)
        {
            for (int s = 0; s < y.Length; s++)
            {
                var lineSeries = new LinearBarSeries { BarWidth = barWidth, FillColor = colorValue[s] };
                for (int i = 0; i < x[0].Length; i++)
                {
                    lineSeries.Points.Add(new DataPoint(x[s][i], y[s][i]));
                }
                model.Series.Add(lineSeries);
            }
            return model;
        }

        #endregion

    }
}
