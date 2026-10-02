I.Phần lý thuyết và câu hỏi ngắn
 Câu 1. Trình bày sự khác nhau giữa Value Types (kiểu giá trị) và Reference Types (kiểu tham chiếu) trong C# về cơ chế lưu trữ vùng nhớ Stack và Heap
Trong C#, kiểu dữ liệu được chia thành hai nhóm cơ bản là Value Types (kiểu giá trị) và Reference Types (kiểu tham chiếu). Hai nhóm này khác nhau chủ yếu ở cách dữ liệu được lưu trữ trong bộ nhớ và cách các biến được sao chép, truyền vào phương thức cũng như quản lý trong quá trình chương trình thực thi.
1. Kiểu giá trị – Value Types
Value Type là kiểu dữ liệu mà biến lưu trực tiếp giá trị của dữ liệu. Khi một biến Value Type được gán cho một biến khác thì giá trị được sao chép sang biến mới. Hai biến sau khi sao chép sẽ độc lập với nhau.
Các kiểu Value Type phổ biến trong C# gồm:
•	int 
•	float 
•	double 
•	decimal 
•	bool 
•	char 
•	struct 
•	enum
2. Kiểu tham chiếu – Reference Types
Reference Type là kiểu dữ liệu mà biến không trực tiếp chứa toàn bộ dữ liệu của đối tượng, mà chứa một tham chiếu (reference) đến đối tượng được lưu trữ trong bộ nhớ.
Các Reference Type thường gặp gồm:
•	class 
•	object 
•	string 
•	array 
•	interface 
•	delegate
Kết luận
Có thể ghi nhớ:
Value Type chứa giá trị, còn Reference Type chứa tham chiếu. Value Type khi gán sẽ tạo ra một bản sao độc lập của giá trị. Reference Type khi gán thường tạo ra một bản sao của tham chiếu, do đó nhiều biến có thể cùng tham chiếu đến một đối tượng trên Heap.

Câu 2. Tính năng Init-only Properties (init) trong C# 9/10 khác gì so với thuộc tính có set thông thường? Nêu trường hợp sử dụng thực tế
Trong C#, Property được sử dụng để kiểm soát việc truy cập và thay đổi dữ liệu của một đối tượng. Hai cách khai báo Property thường gặp là sử dụng set và sử dụng init.
1. Property sử dụng set
Khi khai báo:
public string Name { get; set; }
thì thuộc tính Name có thể được đọc bằng get và thay đổi bằng set.
Ví dụ:
Student sv = new Student();
sv.Name = "Nguyen Van An";
sv.Name = "Tran Van Binh";
Cả hai lần gán đều hợp lệ.
Như vậy, set cho phép thuộc tính được thay đổi trong suốt vòng đời của đối tượng.
Có thể hiểu:
Tạo đối tượng
     ↓
Name = "An"
      ↓
Name = "Bình"
      ↓
Name = "Cường"
      ↓
Có thể tiếp tục thay đổi
Điều này phù hợp với những dữ liệu có thể thay đổi trong quá trình chương trình hoạt động.
Ví dụ:
•	Tên khách hàng có thể được cập nhật. 
•	Địa chỉ khách hàng có thể thay đổi. 
•	Số điện thoại có thể thay đổi. 
•	Trạng thái đơn hàng có thể thay đổi.
2. Property sử dụng 
init được giới thiệu từ C# 9 và tiếp tục được sử dụng trong các phiên bản C# sau đó.
Ví dụ:
public string Name { get; init; }
Property này vẫn có thể được thiết lập khi khởi tạo đối tượng:
Student sv = new Student
{
Name = "Nguyen Van An"
};
Tuy nhiên, sau khi quá trình khởi tạo hoàn thành:
sv.Name = "Tran Van Binh";
sẽ không được phép.
Như vậy, init tạo ra một Property có tính chất gần với chỉ thiết lập một lần.
3. So sánh set và init
Sử dụng set
class Student
{
    public string Name { get; set; }
}
Có thể:
Student sv = new Student
{
    Name = "An"
};

sv.Name = "Binh";
Sử dụng init
class Student
{
    public string Name { get; init; }
}
Có thể:
Student sv = new Student
{
    Name = "An"
};
Nhưng:
sv.Name = "Binh";
sẽ bị lỗi.
5. Một số trường hợp sử dụng thực tế
init phù hợp với:
•	Mã sinh viên. 
•	Mã nhân viên. 
•	Mã đơn hàng. 
•	ID của đối tượng. 
•	Thời điểm tạo đối tượng. 
•	Các thông tin cấu hình. 
•	Các thuộc tính xác định danh tính của đối tượng. 
Ví dụ:
class Order
{
    public int OrderId { get; init; }
    public DateTime CreatedDate { get; init; }
    public string CustomerName { get; set; }
}
OrderId và CreatedDate có thể được thiết lập lúc tạo đơn hàng nhưng không nên tùy ý thay đổi sau đó.

Câu 3. Phân biệt phương thức virtual ở lớp cha và phương thức override ở lớp con khi triển khai tính Đa hình (Polymorphism)
Đa hình (Polymorphism) là một trong những đặc điểm quan trọng của lập trình hướng đối tượng. Trong C#, đa hình cho phép cùng một lời gọi phương thức nhưng có thể thực hiện những hành vi khác nhau tùy thuộc vào đối tượng thực tế.
Để triển khai đa hình giữa lớp cha và lớp con, C# thường sử dụng hai từ khóa:
•	virtual 
•	override 
•	________________________________________
1. Phương thức virtual ở lớp cha
virtual được sử dụng để khai báo một phương thức trong lớp cha có khả năng được ghi đè bởi lớp con.
Ví dụ:
class Animal
{
    public virtual void Sound()
   {
        Console.WriteLine("Dong vat phat ra am thanh");
    }
}
Ở đây:
public virtual void Sound()
có nghĩa là lớp Animal đã cung cấp một cách thực hiện mặc định cho phương thức Sound, đồng thời cho phép lớp dẫn xuất thay đổi cách thực hiện phương thức này.
Có thể hiểu:
virtual là cơ chế mở rộng của lớp cha, cho phép lớp con định nghĩa lại hành vi.
2. Phương thức override ở lớp con
Khi lớp con muốn thay đổi cách thực hiện phương thức virtual của lớp cha, lớp con sử dụng từ khóa override.
Ví dụ:
class Dog : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Cho keu: Gau gau");
    }
}
Ở đây, Dog kế thừa từ Animal.
Phương thức:
override void Sound()
ghi đè cách thực hiện của Sound() trong lớp cha.
Tương tự:
class Cat : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Meo keu: Meo meo");
    }
}
Câu 4. Tại sao một thành phần được khai báo là static trong Class lại không thể truy xuất thông qua một Object Instance được tạo bằng toán tử new?
Trong C#, từ khóa static được sử dụng để khai báo một thành viên thuộc về lớp (Class) thay vì thuộc về từng đối tượng (Object Instance).
Đây là một khái niệm quan trọng trong lập trình hướng đối tượng và cần phân biệt rõ giữa thành viên tĩnh (static member) và thành viên thể hiện (instance member).
1. Thành viên thông thường thuộc về Object
Ví dụ:
class Student
{
    public string Name;
}
Name là thành viên thông thường.
Khi tạo:
Student sv1 = new Student();
Student sv2 = new Student();
chương trình tạo ra hai đối tượng khác nhau.
Có thể:
sv1.Name = "An";
sv2.Name = "Binh";
Kết quả:
sv1.Name = An
sv2.Name = Binh
Mỗi Object có một vùng dữ liệu riêng cho các thành viên instance.
2. Thành viên static thuộc về Class
Nếu khai báo:
class Student
{
    public static int Count;
}
thì Count là thành viên static.
Nó không thuộc riêng về sv1, sv2 hay bất kỳ object nào.
Nó thuộc về:
Student
Do đó cách truy cập đúng là:
Student.Count
chứ về mặt ngữ nghĩa không phải:
sv1.Count
sv2.Count
3. Tại sao static không phụ thuộc vào new?
Khi thực hiện:
Student sv = new Student();
toán tử new tạo ra một instance mới của lớp Student.
Nếu có:	
Student sv1 = new Student();
Student sv2 = new Student();
thì có hai instance:
Student Object 1
Student Object 2
Các thành viên instance có thể khác nhau:
sv1.Name = "An"
sv2.Name = "Binh"
Nhưng thành viên static chỉ có một bản dùng chung cho lớp:
Student
   |
   └── Count
Cho dù tạo:
new Student();
new Student();
new Student();
thì static Count vẫn là thành viên chung của Student.













