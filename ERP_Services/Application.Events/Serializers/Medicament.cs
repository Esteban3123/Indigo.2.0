using Application.Events.Models;
using Domain.Entities;
using System;

namespace Application.Events.Serializers
{
    public interface IDittoDocument
    {
        object Generate(object obj, QueueParameter parameter);
    }

    public class Medicament : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            ATC atc = obj as ATC;
            MMedicament medicament = new MMedicament();
            MethodsMedicament methodsMedicament = new MethodsMedicament();

            if (atc == null) { return medicament; }

            medicament.DCICode = methodsMedicament.DCICode(atc.DCIId);
            medicament.ATCEntityCode = methodsMedicament.ATCEntityCode(atc.ATCEntityId);
            medicament.Code = atc.Code;
            medicament.Name = atc.Name;
            medicament.AbbreviationName = atc.AbbreviationName;
            medicament.AdministrationRoute = methodsMedicament.GenerateAdministrationRoute(atc);
            medicament.ATCAdministrationRoute = methodsMedicament.GenerateATCAdministrationRoute(atc);
            medicament.PharmacologicalGroup = methodsMedicament.GeneratePharmacologicalGroup(atc.PharmacologicalGroupId);
            medicament.Presentations = atc.Presentations;
            medicament.Concentration = atc.Concentration;
            medicament.InventoryRiskLevel = methodsMedicament.GetInventoryRiskLevel(atc.InventoryRiskLevelId);
            medicament.StabilityMinimumHours = atc.StabilityMinimumHours;
            medicament.StabilityMaximumHours = atc.StabilityMaximumHours;
            medicament.FormulationType = atc.FormulationType;
            medicament.Weight = atc.Weight;
            medicament.WeightMeasureUnit = methodsMedicament.GenerateWeightMeasureUnit(atc.WeightMeasureUnit);
            medicament.Volume = atc.Volume;
            medicament.VolumeMeasureUnit = methodsMedicament.GenerateWeightMeasureUnit(atc.VolumeMeasureUnit);
            medicament.AdministrationUnit = methodsMedicament.GenerateWeightMeasureUnit(atc.AdministrationUnitId);
            medicament.Warning = atc.Warning;
            medicament.WarningHtml = atc.WarningHtml;
            medicament.Dosage = atc.Dosage;
            medicament.DosageHtml = atc.DosageHtml;
            medicament.Indications = atc.Indications;
            medicament.IndicationsHtml = atc.IndicationsHtml;
            medicament.ContraIndications = atc.ContraIndications;
            medicament.ContraIndicationsHtml = atc.ContraIndicationsHtml;
            medicament.Precautions = atc.Precautions;
            medicament.PrecautionsHtml = atc.PrecautionsHtml;
            medicament.AdverseReactions = atc.AdverseReactions;
            medicament.AdverseReactionsHtml = atc.AdverseReactionsHtml;
            medicament.AutomaticCalculation = Convert.ToInt16(atc.AutomaticCalculation);
            medicament.TransferSurplusProduct = Convert.ToInt16(atc.TransferSurplusProduct);
            medicament.DiluentProduct = Convert.ToInt16(atc.DiluentProduct);
            medicament.SupplieProduct = Convert.ToInt16(atc.SupplieProduct);
            medicament.JustificationForSpecialDrugs = Convert.ToInt16(atc.JustificationForSpecialDrugs);
            medicament.JustificationOfInputs = Convert.ToInt16(atc.JustificationOfInputs);
            medicament.IndicatorDrug = Convert.ToInt16(atc.IndicatorDrug);
            medicament.POSProduct = Convert.ToInt16(atc.POSProduct);
            medicament.AllPOSPathologies = Convert.ToInt16(atc.AllPOSPathologies);
            medicament.BillingGroupNoPos = methodsMedicament.GenerateBillingGroupNoPos(atc.BillingGroupNoPosId);
            medicament.Status = Convert.ToInt16(atc.Status);
            medicament.CreationUser = atc.CreationUser;
            medicament.CreationDate = Convert.ToString(atc.CreationDate);
            medicament.ModificationUser = atc.ModificationUser;
            medicament.ModificationDate = Convert.ToString(atc.ModificationDate);
            medicament.ProductNPT = Convert.ToInt16(atc.ProductNPT);
            medicament.Osmolarity = atc.Osmolarity;
            medicament.Density = atc.Density;
            medicament.Consumption = Convert.ToInt16(atc.Consumption);
            medicament.PharmaceuticalForm = methodsMedicament.GeneratePharmaceuticalForm(atc.PharmaceuticalFormId);
            medicament.HasSupplieMedicine = Convert.ToInt16(atc.HasSupplieMedicine);
            medicament.RelatedSupplieMedicine = methodsMedicament.GenerateRelatedSupplieMedicine(atc);
            medicament.Antibiotic = Convert.ToInt16(atc.Antibiotic);
            medicament.RequireMedicalBoard = Convert.ToInt16(atc.RequireMedicalBoard);
            medicament.HighCost = Convert.ToInt16(atc.HighCost);
            medicament.DefineProfessional = Convert.ToInt16(atc.DefineProfessional);
            medicament.ClinicalJustification = atc.ClinicalJustification;
            medicament.Conditioned = Convert.ToInt16(atc.Conditioned);
            medicament.UNIRS = Convert.ToInt16(atc.UNIRS);
            medicament.Multidose = Convert.ToByte(atc.Multidose);
            medicament.Stability = Convert.ToByte(atc.Stability);
            medicament.SuitableForReconstitution = Convert.ToByte(atc.SuitableForReconstitution);
            medicament.Combined = Convert.ToByte(atc.Combined);
            medicament.ConcentrationQuantity = atc.ConcentrationQuantity;
            medicament.ConcentrationMeasureUnitCode = methodsMedicament.ConcentrationMeasureUnitCode(atc.ConcentrationMeasureUnitId);
            medicament.UPRUnitsCode = methodsMedicament.UPRUnitsCode(atc.UPRUnitsId);
            return medicament;
        }
    }
}

