using Application.Events.Models;
using Application.Events.Models.Product;
using Domain.Entities;
using System;

namespace Application.Events.Serializers
{
    public class Product : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.InventoryProduct productEntity = obj as Domain.Entities.InventoryProduct;

            MProduct product = new MProduct();
            MethodsProduct methodsProduct = new MethodsProduct();
            product.Code = productEntity.Code;
            product.Name = productEntity.Name;
            product.ProductType = methodsProduct.GetProductType(productEntity.ProductTypeId);
            product.ATC = methodsProduct.GetATC(productEntity.ATCId ?? 0);
            product.CodeCUM = productEntity.CodeCUM;
            product.CodeAlternative = productEntity.CodeAlternative;
            product.CodeAlternativeTwo = productEntity.CodeAlternativeTwo;
            product.Description = productEntity.Description;
            product.ProductGroup = null;
            if (productEntity?.ProductGroupId != null) { product.ProductGroup = methodsProduct.GetProductGroup(productEntity.ProductGroupId.Value); }
            product.ProductSubGroup = null;
            if (productEntity?.ProductSubGroupId != null) { product.ProductSubGroup = methodsProduct.GetProductSubGroup(productEntity.ProductSubGroupId.Value); }          
            product.MeasurementUnit = methodsProduct.GetMeasurementUnit(productEntity.MeasurementUnitId ?? 0);
            product.PackagingUnit = methodsProduct.GetPackagingUnit(productEntity.PackagingUnitId);
            product.Manufacturer = methodsProduct.GetManufacturer(productEntity.ManufacturerId ?? 0);
            product.IVA = methodsProduct.GetIVA(productEntity.IVAId ?? 0);
            product.Presentation = productEntity.Presentation;
            product.CodeSICE = productEntity.CodeSICE;
            product.HandlesSerial = Convert.ToInt16(productEntity.HandlesSerial);
            product.HandlesHealthRegistration = Convert.ToInt16(productEntity.HandlesHealthRegistration);
            product.HealthRegistration = productEntity.HealthRegistration;
            product.ExpirationDate = productEntity.ExpirationDate != null ? Convert.ToString((Convert.ToDateTime(productEntity.ExpirationDate) - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds) : null;
            product.BillingGroup = methodsProduct.GetBillingGroup(productEntity.BillingGroupId ?? 0);
            product.ProductControl = Convert.ToInt16(productEntity.ProductControl);
            product.ProductWithPriceControl = Convert.ToInt16(productEntity.ProductWithPriceControl);
            product.POSProduct = Convert.ToInt16(productEntity.POSProduct);
            product.AuthorizationByOrderNumber = Convert.ToInt16(productEntity.AuthorizationByOrderNumber);
            product.ExpirationDay = Convert.ToInt16(productEntity.ExpirationDay);
            product.MaximumControlPeriod = Convert.ToInt16(productEntity.MaximumControlPeriod);
            product.ControlDays = productEntity.ControlDays;
            product.ControlOrderQuantity = Convert.ToInt16(productEntity.ControlOrderQuantity);
            product.ProductOrderAmount = productEntity.ProductOrderAmount;
            product.LastPurchase = Convert.ToString((Convert.ToDateTime(productEntity.LastPurchase) - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
            product.LastSale = Convert.ToString((Convert.ToDateTime(productEntity.LastSale) - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
            product.ProductOrigin = Convert.ToInt16(productEntity.ProductOrigin);
            product.MinimumStock = productEntity.MinimumStock;
            product.MaximumStock = productEntity.MaximumStock;
            product.CommissionPercentage = productEntity.CommissionPercentage;
            product.RepositionPoint = productEntity.RepositionPoint;
            product.ResetTime = productEntity.ResetTime;
            product.CurrencyType = productEntity.CurrencyType;
            product.ProductCost = productEntity.ProductCost;
            product.FinalProductCost = productEntity.FinalProductCost;
            product.SellingPrice = productEntity.SellingPrice;
            product.AllPOSPathologies = Convert.ToInt16(productEntity.AllPOSPathologies);
            product.Status = Convert.ToInt16(productEntity.Status);
            product.CreationUser = productEntity.CreationUser;
            product.CreationDate = Convert.ToString(productEntity.CreationDate);
            product.ModificationUser = productEntity.ModificationUser;
            product.ModificationDate = Convert.ToString(productEntity.ModificationDate);
            product.BillingGroupNoPos = methodsProduct.GetPBillingGroupNoPos(productEntity.BillingGroupNoPosId ?? 0);
            product.ControlCostPercentage = productEntity.ControlCostPercentage;
            product.InventoryRiskLevel = methodsProduct.GetInventoryRiskLevel(productEntity.InventoryRiskLevelId ?? 0);
            product.SerialNumber = productEntity.SerialNumber;
            product.DriveUnit = productEntity.DriveUnit;
            product.MinimumTemperature = productEntity.MinimumTemperature;
            product.MaximumTemperature = productEntity.MaximumTemperature;
            product.SanitaryRegistration = productEntity.SanitaryRegistration;
            product.Consumption = Convert.ToInt16(productEntity.Consumption);
            product.JustificationSuppliesDispositives = Convert.ToInt16(productEntity.JustificationSuppliesDispositives);
            product.OsteosynthesisMaterial = Convert.ToInt16(productEntity.OsteosynthesisMaterial);
            product.Abbreviation = productEntity.Abbreviation;
            product.Supplie = methodsProduct.GetSupplie(productEntity.SupplieId ?? 0);
            product.IUM = productEntity.IUM;
            product.Storage = productEntity.Storage;
            product.TaxedProduct = Convert.ToByte(productEntity.TaxedProduct);
            product.Osmolarity = productEntity.Osmolarity;
            product.LiquidateSalesTaxes = Convert.ToByte(productEntity.LiquidateSalesTaxes);
            product.SismedReport = Convert.ToByte(productEntity.SismedReport);
            product.DairyComponent = Convert.ToByte(productEntity.DairyComponent);
            product.DairyComponentType = productEntity.DairyComponentType;
            product.ProductBarcode = methodsProduct.GetBarcodes(productEntity);
            return product;
        }
    }
}
