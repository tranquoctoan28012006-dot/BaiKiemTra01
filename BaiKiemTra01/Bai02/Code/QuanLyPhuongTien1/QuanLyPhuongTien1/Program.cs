using System;
using System.Collections.Generic;
using System.Linq;

abstract class PhuongTien
{
    private string _maPT;
    private string _tenHang;
    private int _namSanXuat;
    private decimal _giaGoc;

    public string MaPT
    {
        get { return _maPT; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                _maPT = "PT000";
            else
                _maPT = value;
        }
    }

    public string TenHang
    {
        get { return _tenHang; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Ten hang khong duoc de trong!");

            _tenHang = value;
        }
    }

    public int NamSanXuat
    {
        get { return _namSanXuat; }
        set
        {
            if (value < 1900 || value > DateTime.Now.Year)
                throw new ArgumentException(
                    "Nam san xuat khong hop le!"
                );

            _namSanXuat = value;
        }
    }

    public decimal GiaGoc
    {
        get { return _giaGoc; }
        set
        {
            if (value <= 0)
                throw new ArgumentException(
                    "Gia goc phai lon hon 0!"
                );

            _giaGoc = value;
        }
    }

    public PhuongTien(
        string maPT,
        string tenHang,
        int namSanXuat,
        decimal giaGoc)
    {
        MaPT = maPT;
        TenHang = tenHang;
        NamSanXuat = namSanXuat;
        GiaGoc = giaGoc;
    }

    public abstract decimal TinhGiaLanBanh();

    public virtual string GetInfo()
    {
        return $"Ma PT: {MaPT} | " +
               $"Hang: {TenHang} | " +
               $"Nam SX: {NamSanXuat} | " +
               $"Gia goc: {GiaGoc:N0} VND";
    }
}

class OTo : PhuongTien
{
    private int _soChoNgoi;
    private double _dungTichDongCo;

    public int SoChoNgoi
    {
        get { return _soChoNgoi; }
        set
        {
            if (value <= 0)
                throw new ArgumentException(
                    "So cho ngoi phai lon hon 0!"
                );

            _soChoNgoi = value;
        }
    }

    public double DungTichDongCo
    {
        get { return _dungTichDongCo; }
        set
        {
            if (value <= 0)
                throw new ArgumentException(
                    "Dung tich dong co phai lon hon 0!"
                );

            _dungTichDongCo = value;
        }
    }

    public OTo(
        string maPT,
        string tenHang,
        int namSanXuat,
        decimal giaGoc,
        int soChoNgoi,
        double dungTichDongCo)
        : base(maPT, tenHang, namSanXuat, giaGoc)
    {
        SoChoNgoi = soChoNgoi;
        DungTichDongCo = dungTichDongCo;
    }

    public override decimal TinhGiaLanBanh()
    {
        if (SoChoNgoi <= 9)
        {
            return GiaGoc
                   + GiaGoc * 0.12m
                   + GiaGoc * 0.30m;
        }

        return GiaGoc + GiaGoc * 0.10m;
    }

    public override string GetInfo()
    {
        return base.GetInfo()
             + $" | So cho: {SoChoNgoi}"
             + $" | Dung tich dong co: {DungTichDongCo} L";
    }
}

class XeMay : PhuongTien
{
    private int _dungTichXylanh;

    public int DungTichXylanh
    {
        get { return _dungTichXylanh; }
        set
        {
            if (value <= 0)
                throw new ArgumentException(
                    "Dung tich xi-lanh phai lon hon 0!"
                );

            _dungTichXylanh = value;
        }
    }

    public XeMay(
        string maPT,
        string tenHang,
        int namSanXuat,
        decimal giaGoc,
        int dungTichXylanh)
        : base(maPT, tenHang, namSanXuat, giaGoc)
    {
        DungTichXylanh = dungTichXylanh;
    }

    public override decimal TinhGiaLanBanh()
    {
        if (DungTichXylanh < 175)
            return GiaGoc + GiaGoc * 0.02m;

        return GiaGoc + GiaGoc * 0.05m;
    }

    public override string GetInfo()
    {
        return base.GetInfo()
             + $" | Dung tich xi-lanh: {DungTichXylanh} cc";
    }
}

class QuanLyPhuongTien
{
    private List<PhuongTien> danhSach =
        new List<PhuongTien>();

    public void AddPhuongTien(PhuongTien pt)
    {
        danhSach.Add(pt);
    }

    public void DisplayAll()
    {
        if (danhSach.Count == 0)
        {
            Console.WriteLine("Danh sach dang trong!");
            return;
        }

        Console.WriteLine("\n===== DANH SACH PHUONG TIEN =====");

        foreach (PhuongTien pt in danhSach)
        {
            Console.WriteLine(pt.GetInfo());

            Console.WriteLine(
                $"Gia lan banh: {pt.TinhGiaLanBanh():N0} VND"
            );

            Console.WriteLine("--------------------------------");
        }
    }

    public PhuongTien FindMaxGiaLanBanh()
    {
        if (danhSach.Count == 0)
            return null;

        return danhSach
            .OrderByDescending(
                pt => pt.TinhGiaLanBanh())
            .First();
    }

    public List<PhuongTien> SearchByName(string keyword)
    {
        return danhSach
            .Where(pt =>
                pt.TenHang.ToLower().Contains(keyword.ToLower()))
            .ToList();
    }
}

class Program
{
    static void Main()
    {
        QuanLyPhuongTien quanLy =
            new QuanLyPhuongTien();

        Console.WriteLine("======================================");
        Console.WriteLine("   HE THONG QUAN LY PHUONG TIEN");
        Console.WriteLine("======================================");

        Console.Write("Nhap so luong phuong tien: ");

        int n;

        while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
        {
            Console.Write("Vui long nhap so nguyen duong: ");
        }

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(
                $"\n========== PHUONG TIEN {i + 1} =========="
            );

            Console.WriteLine("1. O to");
            Console.WriteLine("2. Xe may");

            Console.Write("Chon loai: ");
            int loai = int.Parse(Console.ReadLine());

            try
            {
                Console.Write("Nhap ma phuong tien: ");
                string maPT = Console.ReadLine();

                Console.Write("Nhap ten hang: ");
                string tenHang = Console.ReadLine();

                Console.Write("Nhap nam san xuat: ");
                int namSanXuat =
                    int.Parse(Console.ReadLine());

                Console.Write("Nhap gia goc: ");
                decimal giaGoc =
                    decimal.Parse(Console.ReadLine());

                if (loai == 1)
                {
                    Console.Write("Nhap so cho ngoi: ");
                    int soCho =
                        int.Parse(Console.ReadLine());

                    Console.Write(
                        "Nhap dung tich dong co (L): "
                    );

                    double dungTich =
                        double.Parse(Console.ReadLine());

                    OTo oto = new OTo(
                        maPT,
                        tenHang,
                        namSanXuat,
                        giaGoc,
                        soCho,
                        dungTich
                    );

                    quanLy.AddPhuongTien(oto);

                    Console.WriteLine(
                        "Them o to thanh cong!"
                    );

                    Console.WriteLine(
                        "\n===== TINH GIA LAN BANH ====="
                    );

                    if (soCho <= 9)
                    {
                        Console.WriteLine(
                            "Cong thuc: Gia goc + 12% le phi truoc ba + 30% thue tieu thu dac biet"
                        );

                        Console.WriteLine(
                            $"Gia lan banh = {giaGoc:N0} + {giaGoc * 0.12m:N0} + {giaGoc * 0.30m:N0}"
                        );
                    }
                    else
                    {
                        Console.WriteLine(
                            "Cong thuc: Gia goc + 10% le phi truoc ba"
                        );

                        Console.WriteLine(
                            $"Gia lan banh = {giaGoc:N0} + {giaGoc * 0.10m:N0}"
                        );
                    }

                    Console.WriteLine(
                        $"Ket qua: {oto.TinhGiaLanBanh():N0} VND"
                    );
                }
                else if (loai == 2)
                {
                    Console.Write(
                        "Nhap dung tich xi-lanh (cc): "
                    );

                    int xilanh =
                        int.Parse(Console.ReadLine());

                    XeMay xeMay = new XeMay(
                        maPT,
                        tenHang,
                        namSanXuat,
                        giaGoc,
                        xilanh
                    );

                    quanLy.AddPhuongTien(xeMay);

                    Console.WriteLine(
                        "Them xe may thanh cong!"
                    );

                    Console.WriteLine(
                        "\n===== TINH GIA LAN BANH ====="
                    );

                    if (xilanh < 175)
                    {
                        Console.WriteLine(
                            "Cong thuc: Gia goc + 2% thue truoc ba"
                        );

                        Console.WriteLine(
                            $"Gia lan banh = {giaGoc:N0} + {giaGoc * 0.02m:N0}"
                        );
                    }
                    else
                    {
                        Console.WriteLine(
                            "Cong thuc: Gia goc + 5% thue truoc ba"
                        );

                        Console.WriteLine(
                            $"Gia lan banh = {giaGoc:N0} + {giaGoc * 0.05m:N0}"
                        );
                    }

                    Console.WriteLine(
                        $"Ket qua: {xeMay.TinhGiaLanBanh():N0} VND"
                    );
                }
                else
                {
                    Console.WriteLine(
                        "Loai phuong tien khong hop le!"
                    );

                    i--;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    "\nKET QUA KIEM THU:"
                );

                Console.WriteLine(
                    "ArgumentException: " + ex.Message
                );

                Console.WriteLine(
                    "Khong tao duoc doi tuong."
                );

                i--;
            }
        }

        int luaChon;

        do
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("                MENU");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Hien thi tat ca phuong tien");
            Console.WriteLine("2. Tim gia lan banh cao nhat");
            Console.WriteLine("3. Tim theo ten hang");
            Console.WriteLine("0. Thoat");
            Console.WriteLine("======================================");

            Console.Write("Nhap lua chon: ");
            luaChon = int.Parse(Console.ReadLine());

            switch (luaChon)
            {
                case 1:
                    quanLy.DisplayAll();
                    break;

                case 2:
                    PhuongTien max =
                        quanLy.FindMaxGiaLanBanh();

                    if (max != null)
                    {
                        Console.WriteLine(
                            "\n===== GIA LAN BANH CAO NHAT ====="
                        );

                        Console.WriteLine(
                            max.GetInfo()
                        );

                        Console.WriteLine(
                            $"Gia lan banh: {max.TinhGiaLanBanh():N0} VND"
                        );
                    }

                    break;

                case 3:
                    Console.Write(
                        "Nhap ten hang can tim: "
                    );

                    string keyword =
                        Console.ReadLine();

                    List<PhuongTien> ketQua =
                        quanLy.SearchByName(keyword);

                    Console.WriteLine(
                        "\n===== KET QUA TIM KIEM ====="
                    );

                    if (ketQua.Count == 0)
                    {
                        Console.WriteLine(
                            "Khong tim thay phuong tien."
                        );
                    }
                    else
                    {
                        foreach (PhuongTien pt in ketQua)
                        {
                            Console.WriteLine(
                                pt.GetInfo()
                            );

                            Console.WriteLine(
                                $"Gia lan banh: {pt.TinhGiaLanBanh():N0} VND"
                            );
                        }
                    }

                    break;

                case 0:
                    Console.WriteLine(
                        "Da thoat chuong trinh."
                    );
                    break;

                default:
                    Console.WriteLine(
                        "Lua chon khong hop le!"
                    );
                    break;
            }

        } while (luaChon != 0);
    }
}