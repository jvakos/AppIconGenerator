using System;
using System.IO;
using Terminal.Gui;

namespace AppIconGenerator {
    class Program {
        static void Main(string[] args) {
            Application.Init();

            var win = new Window();
            var iconPath = new TextField();
            var fileMask= new TextField();
            var list = new ListView();
            Application.Top.Add(win);
            var dimensions = GetDimensions();


            var lblLogo = new Label();
            lblLogo.Y = 1;
            lblLogo.X = Pos.Percent(25);
            lblLogo.Height = 10;
            lblLogo.Width = 80;
            lblLogo.Text = @"
███╗░░██╗░█████╗░███╗░░██╗░█████╗░░██████╗░█████╗░███████╗████████╗
████╗░██║██╔══██╗████╗░██║██╔══██╗██╔════╝██╔══██╗██╔════╝╚══██╔══╝
██╔██╗██║███████║██╔██╗██║██║░░██║╚█████╗░██║░░██║█████╗░░░░░██║░░░
██║╚████║██╔══██║██║╚████║██║░░██║░╚═══██╗██║░░██║██╔══╝░░░░░██║░░░
██║░╚███║██║░░██║██║░╚███║╚█████╔╝██████╔╝╚█████╔╝██║░░░░░░░░██║░░░
╚═╝░░╚══╝╚═╝░░╚═╝╚═╝░░╚══╝░╚════╝░╚═════╝░░╚════╝░╚═╝░░░░░░░░╚═╝░░░";
            win.Add(lblLogo);




            var mnu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem { Title="_Open", HotKey='O', Action=()=>{
                    var openIconDialog=new OpenDialog("select 1024x1024 icon","");
                    openIconDialog.AllowedFileTypes=new string[]{"png"};
                    Application.Run(openIconDialog);
                    iconPath.Text= openIconDialog.FilePath;
                } },
                new MenuBarItem { Title="_Generate", HotKey='G', Action=()=>{
                    var generateDialog=new SaveDialog("select a directory to place the generated icons","");
                    generateDialog.DirectoryPath=Environment.GetFolderPath( Environment.SpecialFolder.Desktop);
                    Application.Run(generateDialog);                    
                    var iconBuffer= System.IO.File.ReadAllBytes(iconPath.Text.ToString().Trim());
                    string generatedFileBasePath=generateDialog.DirectoryPath.ToString();
                    if (!Directory.Exists(generatedFileBasePath)) Directory.CreateDirectory(generatedFileBasePath);

                    var prg=new ProgressBar(){ Width=10,Height=5 };
                    win.Add(prg);
                    for (int i = 0; i < list.Source.Count; i++) {
                        prg.Pulse();
                        if (list.Source.IsMarked(i)) {
                            var resized=SkiaSharpImageManipulationProvider.Resize(iconBuffer, dimensions[i].Width,dimensions[i].Height);
                            string fileName=fileMask.Text
                                .ToString()
                                .Replace("{W}",resized.Width.ToString())
                                .Replace("{H}",resized.Height.ToString());

                            File.WriteAllBytes( Path.Combine(generatedFileBasePath,fileName), resized.FileContents);
                        }
                    }
                    win.Remove(prg);
                    MessageBox.Query("","App icons generated","OK");
                } },
                new MenuBarItem { Title="Select _All", HotKey='A', Action=()=>{
                    for (int i=0; i<list.Source.Count;i++) {
                        list.Source.SetMark(i,true);
                    }
                    Application.Refresh();
                } },
                new MenuBarItem { Title="_De-Select All", HotKey='D', Action=()=>{
                    for (int i=0; i<list.Source.Count;i++) {
                        list.Source.SetMark(i,false);
                    }
                    Application.Refresh();
                } },
            });

            win.Add(mnu);

            var lblIconpath = new Label {
                Text = "Icon path:",
                Y = Pos.Bottom(lblLogo),
                Width=15
            };
            win.Add(lblIconpath);
            
            iconPath.Y = Pos.Bottom(lblLogo);
            iconPath.X = Pos.Right(lblIconpath);
            iconPath.Width = Dim.Fill();
            win.Add(iconPath);


            var lblMask = new Label("Files pattern:") {Y=Pos.Bottom(iconPath),Width=15 };            
            fileMask = new TextField() { Y = Pos.Bottom(iconPath),X=Pos.Right(lblMask), Width=Dim.Fill(), Text = "Icon{W}x{H}.png" };
            win.Add(lblMask);
            win.Add(fileMask);

            list.SetSource(dimensions);            
            list.Y = Pos.Bottom(fileMask);
            list.X = 2;
            list.AllowsMarking = true;
            list.AllowsMultipleSelection = true;
            list.Width = Dim.Fill();
            list.Height = Dim.Fill();

            win.Add(list);



            

            Application.Run();
        }



        private static IconDimension[] GetDimensions() {
            return new IconDimension[] {
                new IconDimension(16),
                new IconDimension(20),
                new IconDimension(29),
                new IconDimension(32),
                new IconDimension(40),
                new IconDimension(58),
                new IconDimension(60),
                new IconDimension(64),
                new IconDimension(76),
                new IconDimension(80),
                new IconDimension(87),
                new IconDimension(120),
                new IconDimension(128),
                new IconDimension(152),
                new IconDimension(167),
                new IconDimension(180),
                new IconDimension(256),
                new IconDimension(512),
                new IconDimension(1024),
            };
        }




            
    }
}
