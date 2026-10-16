namespace GameOfLife;

public class GameForm : Form
{
    bool[,] cells = new bool[40, 30];
    Random random = new Random();
    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
    Button start = new Button();
    Label status = new Label();
    int generation;
    const int size = 18, top = 75;

    public GameForm()
    {
        Text = "Game of Life";
        ClientSize = new Size(720, 615);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        DoubleBuffered = true;
        start = AddButton("Start", 10, () =>
        { timer.Enabled = !timer.Enabled; start.Text = timer.Enabled ? "Pause" : "Start"; });
        AddButton("Step", 110, () => { if (!timer.Enabled) NextGeneration(); });
        AddButton("Random", 210, () =>
        {
            Pause(); generation = 0;
            for (int x = 0; x < 40; x++)
                for (int y = 0; y < 30; y++) cells[x, y] = random.Next(4) == 0;
            RefreshBoard();
        });
        AddButton("Clear", 310, () => { Pause(); cells = new bool[40, 30]; generation = 0; RefreshBoard(); });
        AddButton("Glider", 410, () => { Pause(); AddGlider(); RefreshBoard(); });
        status.SetBounds(12, 47, 690, 25);
        Controls.Add(status);
        MouseDown += (s, e) =>
        {
            if (timer.Enabled || e.Button != MouseButtons.Left || e.Y < top) return;
            int x = e.X / size, y = (e.Y - top) / size;
            if (x >= 0 && x < 40 && y >= 0 && y < 30)
            { cells[x, y] = !cells[x, y]; RefreshBoard(); }
        };
        timer.Interval = 140;
        timer.Tick += (s, e) => NextGeneration();
        AddGlider();
        RefreshBoard();
    }

    Button AddButton(string text, int x, Action action)
    {
        Button button = new Button { Text = text, Left = x, Top = 10, Width = 90, Height = 30 };
        button.Click += (s, e) => action();
        Controls.Add(button);
        return button;
    }

    void Pause() { timer.Stop(); start.Text = "Start"; }

    void AddGlider()
    {
        cells[20, 14] = true; cells[21, 15] = true;
        cells[19, 16] = true; cells[20, 16] = true; cells[21, 16] = true;
    }

    void NextGeneration()
    {
        bool[,] next = new bool[40, 30];
        for (int x = 0; x < 40; x++)
        {
            for (int y = 0; y < 30; y++)
            {
                int neighbors = 0;
                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0) continue;
                        int nx = x + dx, ny = y + dy;
                        if (nx >= 0 && nx < 40 && ny >= 0 && ny < 30 && cells[nx, ny]) neighbors++;
                    }
                next[x, y] = neighbors == 3 || (cells[x, y] && neighbors == 2);
            }
        }
        cells = next;
        generation++;
        RefreshBoard();
    }

    void RefreshBoard()
    {
        int alive = 0;
        foreach (bool cell in cells) if (cell) alive++;
        status.Text = $"Generation: {generation}     Living cells: {alive}     Click cells while paused.";
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        for (int x = 0; x < 40; x++)
            for (int y = 0; y < 30; y++)
            {
                e.Graphics.FillRectangle(cells[x, y] ? Brushes.SeaGreen : Brushes.WhiteSmoke, x * size, top + y * size, size, size);
                e.Graphics.DrawRectangle(Pens.LightGray, x * size, top + y * size, size, size);
            }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) timer.Dispose();
        base.Dispose(disposing);
    }
}
