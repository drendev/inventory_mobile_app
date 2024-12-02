namespace inventory_mobile_app.Pages;
using inventory_mobile_app.ViewModels;
using Microsoft.Maui.Graphics;

public partial class HomePage : ContentPage
{

    public HomePage(DashboardViewModel dashboardViewModel)
    {
        InitializeComponent();
        BindingContext = dashboardViewModel;

        dashboardViewModel.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(dashboardViewModel.Dashboard))
            {
                UpdateDrawable(dashboardViewModel);
            }
        };
    }

    private void UpdateDrawable(DashboardViewModel viewModel)
    {
        if (viewModel.Dashboard != null)
        {
            AvailabilityChart.Drawable = new AvailabilityChartDrawable(
                inStock: viewModel.Dashboard.InStock,
                lowStock: viewModel.Dashboard.LowStock,
                outOfStock: viewModel.Dashboard.OutOfStock
            );
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is DashboardViewModel viewModel)
        {
            viewModel.LoadDashboard();  
        }
    }

    public class AvailabilityChartDrawable : IDrawable
    {
        private readonly float _inStock;
        private readonly float _lowStock;
        private readonly float _outOfStock;

        public AvailabilityChartDrawable(float inStock, float lowStock, float outOfStock)
        {
            _inStock = inStock;
            _lowStock = lowStock;
            _outOfStock = outOfStock;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // Set up the total stock and make sure it's greater than 0
            float total = _inStock + _lowStock + _outOfStock;
            if (total <= 0) return;

            // Define the number of bars and the width of each bar
            float barWidth = dirtyRect.Width / 6; // One-fourth of the width for spacing between bars
            float barSpacing = barWidth / 2; // Spacing between bars

            // Calculate the height of the bars based on the total value
            float maxBarHeight = dirtyRect.Height * 0.8f; // 80% of the available height for bars
            float inStockHeight = (_inStock / total) * maxBarHeight;
            float lowStockHeight = (_lowStock / total) * maxBarHeight;
            float outOfStockHeight = (_outOfStock / total) * maxBarHeight;

            // Define the x-position for the bars
            float startX = dirtyRect.Width * 0.1f; // 10% margin from the left

            // Draw "In Stock" bar (Yellow)
            DrawBar(canvas, startX, dirtyRect.Height - inStockHeight, barWidth, inStockHeight, Color.FromArgb("#FFD700"));

            // Draw "Low Stock" bar (Orange)
            startX += barWidth + barSpacing; // Move to the next x position
            DrawBar(canvas, startX, dirtyRect.Height - lowStockHeight, barWidth, lowStockHeight, Color.FromArgb("#FF6B35"));

            // Draw "Out of Stock" bar (Red)
            startX += barWidth + barSpacing; // Move to the next x position
            DrawBar(canvas, startX, dirtyRect.Height - outOfStockHeight, barWidth, outOfStockHeight, Colors.Red);
        }

        private void DrawBar(ICanvas canvas, float x, float y, float width, float height, Color color)
        {
            canvas.FillColor = color;
            canvas.FillRectangle(x, y, width, height);
        }
    }
}
