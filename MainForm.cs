using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace KuriBlue;

public sealed class MainForm : Form
{
    readonly Color bg = Color.FromArgb(9, 15, 29);
    readonly Color panel = Color.FromArgb(16, 26, 47);
    readonly Color blue = Color.FromArgb(42, 133, 255);
    readonly ListView list = new() { View = View.Details, FullRowSelect = true, GridLines = false, BorderStyle = BorderStyle.None, BackColor = Color.FromArgb(13, 22, 40), ForeColor = Color.FromArgb(226, 237, 255), Font = new Font("Segoe UI", 9) };
    readonly Label count = new();
    readonly Label status = new();
    readonly System.Windows.Forms.Timer timer = new() { Interval = 2500 };
    readonly string[] keywords = { "cheat", "injector", "aimbot", "wallhack", "executor", "memory editor", "trainer" };

    public MainForm()
    {
        Text = "Kuri Monitor";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(1060, 700);
        MinimumSize = new Size(900, 600);
        BackColor = bg;
        ForeColor = Color.White;
        Font = new Font("Segoe UI", 10);
        DoubleBuffered = true;

        var header = new Panel { Dock = DockStyle.Top, Height = 92, BackColor = bg, Padding = new Padding(28, 18, 28, 10) };
        var brand = new Label { Text = "KURI  /  MONITOR", Font = new Font("Segoe UI", 19, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(26, 15) };
        var subtitle = new Label { Text = "LIGHTWEIGHT PROCESS MONITOR  •  READ-ONLY SCAN", Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = Color.FromArgb(112, 155, 215), AutoSize = true, Location = new Point(29, 52) };
        header.Controls.Add(brand); header.Controls.Add(subtitle);

        var top = new Panel { Dock = DockStyle.Top, Height = 118, BackColor = bg, Padding = new Padding(24, 5, 24, 15) };
        var statCard = MakeCard(24, 4, 270, 92);
        var statTitle = new Label { Text = "RUNNING PROCESSES", ForeColor = Color.FromArgb(133, 160, 198), Font = new Font("Segoe UI", 8, FontStyle.Bold), Location = new Point(18, 15), AutoSize = true };
        count.Text = "—";
        count.Font = new Font("Segoe UI", 25, FontStyle.Bold);
        count.ForeColor = Color.White; count.Location = new Point(17, 35); count.AutoSize = true;
        statCard.Controls.Add(statTitle); statCard.Controls.Add(count);

        var infoCard = MakeCard(310, 4, 690, 92);
        var info = new Label { Text = "KURI BLUE  •  SAFE MODE", ForeColor = Color.FromArgb(94, 166, 255), Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(20, 16), AutoSize = true };
        var desc = new Label { Text = "Scans visible process names only. No memory access, injection, or system modification.", ForeColor = Color.FromArgb(164, 183, 211), Location = new Point(20, 43), AutoSize = true };
        status.Text = "●  MONITORING";
        status.ForeColor = Color.FromArgb(91, 220, 165); status.Font = new Font("Segoe UI", 9, FontStyle.Bold); status.Location = new Point(20, 66); status.AutoSize = true;
        infoCard.Controls.Add(info); infoCard.Controls.Add(desc); infoCard.Controls.Add(status);

        var body = new Panel { Dock = DockStyle.Fill, BackColor = bg, Padding = new Padding(24, 4, 24, 22) };
        var bar = new Panel { Dock = DockStyle.Top, Height = 52, BackColor = bg };
        var heading = new Label { Text = "PROCESS ACTIVITY", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.White, Location = new Point(3, 12), AutoSize = true };
        var scan = new Button { Text = "SCAN NOW  ↻", Width = 132, Height = 34, FlatStyle = FlatStyle.Flat, BackColor = blue, ForeColor = Color.White, Font = new Font("Segoe UI", 9, FontStyle.Bold), Anchor = AnchorStyles.Top | AnchorStyles.Right, Location = new Point(820, 7), Cursor = Cursors.Hand };
        scan.FlatAppearance.BorderSize = 0;
        scan.Click += (_, _) => Scan();
        bar.Controls.Add(heading); bar.Controls.Add(scan);

        list.Dock = DockStyle.Fill;
        list.Columns.Add("PROCESS", 330);
        list.Columns.Add("PROCESS ID", 120);
        list.Columns.Add("STATUS", 170);
        list.Columns.Add("STARTED", 220);
        body.Controls.Add(list); body.Controls.Add(bar);

        Controls.Add(body); Controls.Add(top); Controls.Add(header);
        timer.Tick += (_, _) => Scan();
        Shown += (_, _) => Scan();
        timer.Start();
    }

    Panel MakeCard(int x, int y, int w, int h) => new() { Location = new Point(x, y), Size = new Size(w, h), BackColor = panel, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };

    void Scan()
    {
        try
        {
            var processes = Process.GetProcesses().OrderBy(p => p.ProcessName, StringComparer.OrdinalIgnoreCase).ToArray();
            list.BeginUpdate(); list.Items.Clear();
            foreach (var p in processes)
            {
                string name;
                try { name = p.ProcessName; } catch { continue; }
                bool match = keywords.Any(k => name.Contains(k, StringComparison.OrdinalIgnoreCase));
                var item = new ListViewItem(name);
                item.SubItems.Add(p.Id.ToString());
                item.SubItems.Add(match ? "NAME MATCH — REVIEW" : "RUNNING");
                item.SubItems.Add("—");
                if (match) item.ForeColor = Color.FromArgb(255, 190, 105);
                list.Items.Add(item);
            }
            list.EndUpdate();
            count.Text = processes.Length.ToString();
            status.Text = $"●  LAST SCAN  {DateTime.Now:HH:mm:ss}";
        }
        catch (Exception ex) { status.Text = "●  SCAN ERROR: " + ex.Message; status.ForeColor = Color.Salmon; }
    }

    protected override void OnFormClosed(FormClosedEventArgs e) { timer.Stop(); timer.Dispose(); base.OnFormClosed(e); }
}
