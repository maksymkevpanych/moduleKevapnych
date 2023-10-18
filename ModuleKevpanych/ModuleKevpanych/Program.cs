using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

class Transportation
{
    public int CarNumber { get; set; }
    public int BaseNumber { get; set; }
    public DateTime Date { get; set; }
    public int CargoCode { get; set; }
    public decimal Cost { get; set; }
}

class Cargo
{
    public int Code { get; set; }
    public string Name { get; set; }
}

class Program
{
    static void Main()
    {
        int targetCarNumber = 123;
        int currentYear = DateTime.Now.Year;

        List<Transportation> transportations = GetTransportations();

        var cargos = transportations
            .Where(t => t.CarNumber == targetCarNumber && t.Date.Year == currentYear)
            .Select(t => t.CargoCode)
            .Distinct();

        foreach (var cargoCode in cargos)
        {
            Cargo cargo = GetCargoByCode(cargoCode);
            Console.WriteLine($"Код вантажу: {cargo.Code}, Назва вантажу: {cargo.Name}");
        }

        var cargoGroups = transportations
            .GroupBy(t => t.CargoCode)
            .Select(group => new
            {
                CargoCode = group.Key,
                BaseNumber = group.First().BaseNumber,
                TotalCost = group.Sum(t => t.Cost)
            });

        foreach (var cargoGroup in cargoGroups)
        {
            Cargo cargo = GetCargoByCode(cargoGroup.CargoCode);
            Console.WriteLine($"Назва вантажу: {cargo.Name}, Номер автобази: {cargoGroup.BaseNumber}, " +
                              $"Сумарна вартість перевезень: {cargoGroup.TotalCost}");
        }

        List<Cargo> cargosForXml = GetCargos();

        XElement cargoXml = new XElement("Cargos",
            cargosForXml.Select(c => new XElement("Cargo",
                new XElement("Code", c.Code),
                new XElement("Name", c.Name)
            ))
        );

        try
        {
            cargoXml.Save("cargos.xml");
            Console.WriteLine("Дані збережено в файл cargos.xml.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка під час збереження файлу: {ex.Message}");
        }
    }

    static List<Transportation> GetTransportations()
    {

        return new List<Transportation>();
    }

    static Cargo GetCargoByCode(int code)
    {

        return new Cargo();
    }

    static List<Cargo> GetCargos()
    {

        return new List<Cargo>();
    }
}