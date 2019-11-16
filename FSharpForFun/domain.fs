namespace MyApplication.BusinessLogic

type Name = {
    FirstName: string
    MiddleName: string option
    LastName: string
}
type Customer = {
    Name: Name
    Age: int}

type Account = {
    Number: int
    Owner: Customer}

