using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MetaCleaner
{
    static class Program
    {
        public static string ExifPath =>
            Path.Combine(Application.StartupPath, "Resources", "exiftool.exe");

        [STAThread]
        static void Main()
        {
            // ------------------------------
            // 1) Завершаем все старые инстансы
            // ------------------------------
            var me = Process.GetCurrentProcess();
            var existing = Process
                .GetProcessesByName(me.ProcessName)
                .Where(p => p.Id != me.Id);
            foreach (var p in existing)
            {
                try { p.Kill(); }
                catch { /* если не удалось — просто пропускаем */ }
            }
            // ------------------------------

            // Проверяем exiftool.exe
            if (!File.Exists(ExifPath))
            {
                MessageBox.Show(
                    $"Cannot find exiftool.exe at:\r\n{ExifPath}",
                    "MetaCleaner — Missing Dependency",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
