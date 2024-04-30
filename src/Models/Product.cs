using System.ComponentModel.DataAnnotations;

public class Product{

// productID SERIAL PRIMARY KEY,
public Guid ProductID {get;set;}=Guid.NewGuid();

// productName VARCHAR(50) NOT NULL,
public required string Productname {get;set;}

[Required(ErrorMessage ="Productname is required")]
[StringLength(50)]

//   description TEXT,
public string Description {get;set;}=string.Empty;

// quantity INT NOT NULL,
public required int Quantity {get;set;}

//  price DECIMAL(10, 2) NOT NULL,
public required decimal Price {get;set;}


//  categoryID INT,
public required Guid categoryID {get;set;}

//  createdDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
public DateTime CreatedAt { get; set; } = DateTime.Now;


}