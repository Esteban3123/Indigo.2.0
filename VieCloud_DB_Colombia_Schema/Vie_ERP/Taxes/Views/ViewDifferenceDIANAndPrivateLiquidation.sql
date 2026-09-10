

CREATE VIEW [Taxes].[ViewDifferenceDIANAndPrivateLiquidation]
AS

select tp.Id,
tp.Nit as Nit, 
case tp.PersonType when 1 then 'Natural' else 'Jurídico' end as ReasonSocial,
(select top 1 Addresss from Common.[Address] where IdPerson = tp.PersonId) as Addresss,
(select top 1 Phone from Common.[Phone] where IdPerson = tp.PersonId) as Phone, 
0 as ValueOne, 
stri.TotalTaxableOrdinaryAndExtraordinary as ValueTwo,
(stri.TotalTaxableOrdinaryAndExtraordinary) as DifferenceValue,
2022 as Year
from  Common.ThirdParty as tp
inner join Taxes.SingleTaxReturnForInsdutry as stri on stri.IdentificationNumber = tp.Nit and stri.TaxablePeriod = 2022
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra las diferencias entre la liquidación privada del impuesto de industria y comercio (ICA) y la información reportada ante la DIAN para el período fiscal 2022. Cruza los terceros registrados en el sistema (empresas, contratistas o contribuyentes) con sus declaraciones únicas de ICA, exponiendo el NIT, razón social o tipo de persona (natural o jurídica), dirección y teléfono de contacto, junto con el valor declarado en la liquidación privada y la diferencia resultante respecto al valor DIAN (que en esta versión aparece en cero, indicando que aún no se ha cargado el dato de la DIAN). Sirve para que el área tributaria identifique contribuyentes con discrepancias entre lo liquidado internamente y lo reportado ante la autoridad fiscal, facilitando la conciliación y el control de obligaciones de industria y comercio.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'VIEW', @level1name = N'ViewDifferenceDIANAndPrivateLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'VIEW', @level1name = N'ViewDifferenceDIANAndPrivateLiquidation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Compara, por tercero, la liquidación privada del impuesto de industria y comercio (ICA) declarada ante la DIAN frente a la liquidación interna, exponiendo la diferencia para el periodo 2022.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDifferenceDIANAndPrivateLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El tercero debe existir en Common.ThirdParty con NIT registrado.; Debe existir una declaración en Taxes.SingleTaxReturnForInsdutry cuyo IdentificationNumber coincida con el NIT del tercero y cuyo TaxablePeriod sea 2022.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDifferenceDIANAndPrivateLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El periodo gravable está fijado en 2022 (hardcoded) tanto en el filtro como en la columna Year.; ValueOne siempre es 0, por lo que DifferenceValue equivale al TotalTaxableOrdinaryAndExtraordinary.; Solo se considera la primera dirección y el primer teléfono registrados para la persona (TOP 1 sin ORDER BY).; Solo se incluyen terceros que tengan declaración ICA del periodo 2022 (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDifferenceDIANAndPrivateLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Impuesto de Industria y Comercio (ICA); Liquidación privada vs DIAN; Tercero/Contribuyente; NIT; Persona Natural/Jurídica; Periodo gravable; Base gravable ordinaria y extraordinaria', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDifferenceDIANAndPrivateLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve una fila por tercero con declaración ICA del periodo 2022, incluyendo NIT, tipo (Natural/Jurídico), primera dirección y primer teléfono registrados, valor base 0, total gravable ordinario y extraordinario, y la diferencia (igual al total gravable).', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDifferenceDIANAndPrivateLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si tp.PersonType = 1 → Clasifica al tercero como ''Natural'' else Clasifica al tercero como ''Jurídico''', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDifferenceDIANAndPrivateLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Taxes.SingleTaxReturnForInsdutry; Common.Address; Common.Phone', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDifferenceDIANAndPrivateLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDifferenceDIANAndPrivateLiquidation';
GO
