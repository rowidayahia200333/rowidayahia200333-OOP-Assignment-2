namespace rowidayahia200333_OOP_Assignment_2.Inhertance;

public class Program
{
    public static void Main(string[] args)
    {

        Library library = new Library();

      

        Student testMember =
            new Student("S000", "Test Student", "01000000000");

        Person person = new Person("P1", "Person", "01000000000");


        Member member = new Member("M1", "Member", "01000000000", 3, 0);

        Staff staff = new Staff("ST1", "Staff", "01000000000",
           DateTime.Today, 5000, 0);


        LibraryItem item = new LibraryItem(
           "I1", "Item", 10, 5, 1);


        testMember.FullName = "New Name";

        testMember.Loans.Add(null);


        Book testBook = new Book("B0", "Test Book", 10);
        testBook.IsOnLoan = true;



        Console.WriteLine();
     

        Book book1 = new Book("B1", "C# Book", 10);
        Book book2 = new Book("B2", "OOP Book", 10);
        Book book3 = new Book("B3", "Algorithms Book", 10);
        Book book4 = new Book("B4", "Data Structures Book", 10);

        DVD dvd1 = new DVD("D1", "Inception", 20);

        Magazine magazine1 =
            new Magazine("M1", "Science Magazine", 5);


        Console.WriteLine();
   

        Student student1 =
            new Student("S1", "Ahmed Student", "01011111111");

        book1.Withdraw();

        try
        {
            library.Borrow(
                1,
                new DateTime(2026, 9, 1),
                student1,
                book1);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        book1.Restore();


        Console.WriteLine();
     

        Loan loan1 = library.Borrow(
            2,
            new DateTime(2026, 9, 1),
            student1,
            book1);

        try
        {
            library.Borrow(
                3,
                new DateTime(2026, 9, 2),
                student1,
                book1);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


        Console.WriteLine();
        

        Loan loan2 = library.Borrow(
            4,
            new DateTime(2026, 9, 2),
            student1,
            book2);

        Loan loan3 = library.Borrow(
            5,
            new DateTime(2026, 9, 3),
            student1,
            book3);

        try
        {
            library.Borrow(
                6,
                new DateTime(2026, 9, 4),
                student1,
                book4);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


        Console.WriteLine();
        

        Librarian librarian =
            new Librarian(
                "L1",
                "Ali Librarian",
                "01022222222",
                new DateTime(2025, 1, 1),
                6000);

        HeadLibrarian headLibrarian =
            new HeadLibrarian(
                "HL1",
                "Mona Head",
                "01033333333",
                new DateTime(2024, 1, 1),
                9000);

        Shelver shelver =
            new Shelver(
                "SH1",
                "Omar Shelver",
                "01044444444",
                new DateTime(2026, 1, 1),
                5000,
                "Fiction");

        List<Staff> staffMembers = new List<Staff>();

        staffMembers.Add(librarian);
        staffMembers.Add(headLibrarian);
        staffMembers.Add(shelver);
        [9 / 24 / 2026 5:32 PM] Rowida Yahia: foreach (Staff staff in staffMembers)
        {
            Console.WriteLine(
                staff.FullName + " : " + staff.MonthlyPay);
        }


        Console.WriteLine();
      

        List<LibraryItem> items = new List<LibraryItem>();

        items.Add(book1);
        items.Add(dvd1);
        items.Add(magazine1);

        foreach (LibraryItem item in items)
        {
            Console.WriteLine(
                item.Title
                + " | Loan Period: "
                + item.LoanPeriod
                + " | Daily Late Fee: "
                + item.DailyLateFee);
        }


        Console.WriteLine();
    

        PremiumMember premium =
            new PremiumMember(
                "P1",
                "Sara Premium",
                "01055555555",
                20);

        Loan premiumLoan = library.Borrow(
            7,
            new DateTime(2026, 9, 1),
            premium,
            dvd1);

        Console.WriteLine(
            "Due Date: " + premiumLoan.DueDate);

        DateTime returnDate =
            premiumLoan.DueDate.AddDays(5);

        librarian.ReturnLoan(
            premiumLoan,
            returnDate);

        Console.WriteLine(
            "Late Fee: " + premiumLoan.LateFee);

        Console.WriteLine(
            "Reading Points: " + premium.ReadingPoints);


        Console.WriteLine();
 

        try
        {
            librarian.ReturnLoan(
                premiumLoan,
                returnDate.AddDays(1));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


        Console.WriteLine();
       

        try
        {
            librarian.MarkLoanAsLost(premiumLoan);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


        Console.WriteLine();
    

        try
        {
            librarian.GiveRaise(0);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        try
        {
            headLibrarian.ChangeLateFee(book2, 0);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


        Console.WriteLine();
      

        Student student2 =
            new Student(
                "S2",
                "Another Student",
                "01066666666");

        Loan invalidDateLoan = library.Borrow(
            8,
            new DateTime(2026, 9, 10),
            student2,
            book4);

        try
        {
            librarian.ReturnLoan(
                invalidDateLoan,
                new DateTime(2026, 9, 5));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


        Console.WriteLine();
     
    }



}


public class Person
{
    public string PersonId { get; }
    public string FullName { get; }
    public string Phone { get; }

    protected Person(string personId, string fullName, string phone)
    {
        if (string.IsNullOrEmpty(personId))
            throw new Exception("Person ID cannot be null or empty.");

        if (string.IsNullOrEmpty(fullName))
            throw new Exception("Full name cannot be null or empty.");

        if (string.IsNullOrEmpty(phone))
            throw new Exception("Phone cannot be null or empty.");

        PersonId = personId;
        FullName = fullName;
        Phone = phone;
    }
}

public class Member : Person
{
    private List<Loan> loans = new List<Loan>();

    public IReadOnlyList<Loan> Loans
    {
        get { return loans; }
    }

    protected int MaxLoans { get; }
    public decimal DiscountPercentage { get; }

    protected Member(
        string personId,
        string fullName,
        string phone,
        int maxLoans,
        decimal discountPercentage)
        : base(personId, fullName, phone)
    {
        if (maxLoans <= 0)
            throw new Exception("Maximum loans must be greater than zero.");

        if (discountPercentage < 0 || discountPercentage > 100)
            throw new Exception("Discount percentage must be between 0 and 100.");

        MaxLoans = maxLoans;
        DiscountPercentage = discountPercentage;
    }

    public void Borrow(Loan loan)
    {
        if (loan == null)
            throw new Exception("Loan cannot be null.");

        int activeLoans = 0;

        foreach (Loan existingLoan in loans)
        {
            if (existingLoan.Status == LoanStatus.Borrowed)
            {
                activeLoans++;
            }
        }

        if (activeLoans >= MaxLoans)
            throw new Exception("Member has reached the maximum loan limit.");

        if (loan.Item.IsWithdrawn)
            throw new Exception("A withdrawn item cannot be borrowed.");

        if (loan.Item.IsOnLoan)
            throw new Exception("Item is already on loan.");

        if (loan.Member != this)
            throw new Exception("Loan does not belong to this member.");

        loans.Add(loan);

        loan.Item.IsOnLoan = true;
    }
}
public class Student : Member
{
    public Student(
        string personId,
        string fullName,
        string phone)
        : base(
            personId,
            fullName,
            phone,
            3,
            0)
    {
    }
}
public class PremiumMember : Member
{
    public PremiumMember(
        string personId,
        string fullName,
        string phone,
        decimal discountPercentage)
        : base(
            personId,
            fullName,
            phone,
            10,
            discountPercentage)
    {
    }

    public int ReadingPoints
    {
        get
        {
            int points = 0;

            foreach (Loan loan in Loans)
            {
                if (loan.Status == LoanStatus.Returned)
                {
                    points += 5;
                }
            }

            return points;
        }
    }
}
public class Staff : Person
{
    public DateTime HireDate { get; }

    public decimal MonthlySalary { get; private set; }

    private decimal responsibilityAllowance;

    protected Staff(
        string personId,
        string fullName,
        string phone,
        DateTime hireDate,
        decimal monthlySalary,
        decimal responsibilityAllowance)
        : base(personId, fullName, phone)
    {
        if (monthlySalary < 0)
            throw new Exception("Monthly salary cannot be negative.");

        if (responsibilityAllowance < 0)
            throw new Exception("Responsibility allowance cannot be negative.");

        HireDate = hireDate;
        MonthlySalary = monthlySalary;
        this.responsibilityAllowance = responsibilityAllowance;
    }

    public decimal MonthlyPay
    {
        get
        {
            return MonthlySalary + responsibilityAllowance;
        }
    }

    public void GiveRaise(decimal percentage)
    {
        if (percentage <= 0)
            throw new Exception("Raise percentage must be greater than zero.");

        MonthlySalary += MonthlySalary * percentage / 100;
    }
}

public class Librarian : Staff
{
    public Librarian(
        string personId,
        string fullName,
        string phone,
        DateTime hireDate,
        decimal monthlySalary)
        : base(
            personId,
            fullName,
            phone,
            hireDate,
            monthlySalary,
            0)
    {
    }

    public void ReturnLoan(Loan loan, DateTime returnDate)
    {
        loan.Return(returnDate);
    }

    public void MarkLoanAsLost(Loan loan)
    {
        loan.MarkAsLost();
    }
}

public class Shelver : Staff
{
    public string Section { get; private set; }

    public Shelver(
        string personId,
        string fullName,
        string phone,
        DateTime hireDate,
        decimal monthlySalary,
        string section)
        : base(
            personId,
            fullName,
            phone,
            hireDate,
            monthlySalary,
            0)
    {
        if (string.IsNullOrEmpty(section))
            throw new Exception("Section cannot be null or empty.");

        Section = section;
    }

    public void Reassign(string newSection)
    {
        if (string.IsNullOrEmpty(newSection))
            throw new Exception("Section cannot be null or empty.");

        Section = newSection;
    }
}
public class HeadLibrarian : Staff
{
    public HeadLibrarian(
        string personId,
        string fullName,
        string phone,
        DateTime hireDate,
        decimal monthlySalary)
        : base(
            personId,
            fullName,
            phone,
            hireDate,
            monthlySalary,
            400)
    {
    }

    public void ChangeLateFee(LibraryItem item, decimal newFee)
    {
        if (item == null)
            throw new Exception("Item cannot be null.");

        item.ChangeBaseLateFee(newFee);
    }

    public void WithdrawItem(LibraryItem item)
    {
        if (item == null)
            throw new Exception("Item cannot be null.");

        item.Withdraw();
    }

    public void RestoreItem(LibraryItem item)
    {
        if (item == null)
            throw new Exception("Item cannot be null.");

        item.Restore();
    }
}

public class LibraryItem
{
    public string CatalogNumber { get; }
    public string Title { get; }

    public decimal BaseLateFee { get; private set; }

    public bool IsWithdrawn { get; private set; }

    public bool IsOnLoan { get; internal set; }

    public int LoanPeriod { get; }

    private decimal lateFeeMultiplier;

    protected LibraryItem(
        string catalogNumber,
        string title,
        decimal baseLateFee,
        int loanPeriod,
        decimal lateFeeMultiplier)
    {
        if (string.IsNullOrEmpty(catalogNumber))
            throw new Exception("Catalog number cannot be null or empty.");

        if (string.IsNullOrEmpty(title))
            throw new Exception("Title cannot be null or empty.");

        if (baseLateFee <= 0)
            throw new Exception("Base late fee must be greater than zero.");

        if (loanPeriod <= 0)
            throw new Exception("Loan period must be greater than zero.");

        if (lateFeeMultiplier <= 0)
            throw new Exception("Late fee multiplier must be greater than zero.");

        CatalogNumber = catalogNumber;
        Title = title;
        BaseLateFee = baseLateFee;
        LoanPeriod = loanPeriod;
        lateFeeMultiplier = lateFeeMultiplier;

        IsWithdrawn = false;
        IsOnLoan = false;
    }

    public decimal DailyLateFee
    {
        get
        {
            return BaseLateFee * lateFeeMultiplier;
        }
    }

    public void ChangeBaseLateFee(decimal newFee)
    {
        if (newFee <= 0)
            throw new Exception("New late fee must be greater than zero.");

        BaseLateFee = newFee;
    }

    public void Withdraw()
    {
        IsWithdrawn = true;
    }

    public void Restore()
    {
        IsWithdrawn = false;
    }
}
public class Book : LibraryItem
{
    public Book(
        string catalogNumber,
        string title,
        decimal baseLateFee)
        : base(
            catalogNumber,
            title,
            baseLateFee,
            21,
            1)
    {
    }
}
public class DVD : LibraryItem
{
    public DVD(
        string catalogNumber,
        string title,
        decimal baseLateFee)
        : base(
            catalogNumber,
            title,
            baseLateFee,
            7,
            2)
    {
    }
}
public class Magazine : LibraryItem
{
    public Magazine(
        string catalogNumber,
        string title,
        decimal baseLateFee)
        : base(
            catalogNumber,
            title,
            baseLateFee,
            3,
            0.5m)
    {
    }
}
public enum LoanStatus
{
    Borrowed,
    Returned,
    Lost
}

public class Loan
{
    public int LoanId { get; }

    public DateTime BorrowDate { get; }

    public Member Member { get; }

    public LibraryItem Item { get; }

    public LoanStatus Status { get; private set; }

    public DateTime? ReturnDate { get; private set; }

    public DateTime DueDate
    {
        get
        {
            return BorrowDate.AddDays(Item.LoanPeriod);
        }
    }

    public decimal LateFee
    {
        get
        {
            if (Status != LoanStatus.Returned)
                return 0;

            if (ReturnDate <= DueDate)
                return 0;

            int lateDays = (ReturnDate.Value - DueDate).Days;

            decimal fee = lateDays * Item.DailyLateFee;

            decimal discount = fee * Member.DiscountPercentage / 100;

            return fee - discount;
        }
    }

    public Loan(
        int loanId,
        DateTime borrowDate,
        Member member,
        LibraryItem item)
    {
        if (loanId <= 0)
            throw new Exception("Loan ID must be greater than zero.");

        if (member == null)
            throw new Exception("Member cannot be null.");

        if (item == null)
            throw new Exception("Item cannot be null.");

        if (item.IsWithdrawn)
            throw new Exception("A withdrawn item cannot be borrowed.");

        if (item.IsOnLoan)
            throw new Exception("Item is already on loan.");

        LoanId = loanId;
        BorrowDate = borrowDate;
        Member = member;
        Item = item;

        Status = LoanStatus.Borrowed;
        ReturnDate = null;
    }

    public void Return(DateTime returnDate)
    {
        if (Status != LoanStatus.Borrowed)
            throw new Exception("Only a borrowed loan can be returned.");

        if (returnDate < BorrowDate)
            throw new Exception("Return date cannot be earlier than borrow date.");

        ReturnDate = returnDate;
        Status = LoanStatus.Returned;
        Item.IsOnLoan = false;
    }

    public void MarkAsLost()
    {
        if (Status != LoanStatus.Borrowed)
            throw new Exception("Only a borrowed loan can be marked as lost.");

        Status = LoanStatus.Lost;
        Item.IsOnLoan = false;
    }
}

public class Library
{
    public Loan Borrow(
        int loanId,
        DateTime borrowDate,
        Member member,
        LibraryItem item)
    {
        if (member == null)
            throw new Exception("Member cannot be null.");

        if (item == null)
            throw new Exception("Item cannot be null.");

        Loan loan = new Loan(
            loanId,
            borrowDate,
            member,
            item);

        member.Borrow(loan);

        return loan;
    }
}