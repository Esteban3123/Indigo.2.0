

CREATE VIEW [Taxes].[ViewDiscountReteIcaAndPrivateDeclaration]
AS

select stri.Id,
stri.IdentificationNumber as Nit,
case tp.PersonType when 1 then 'Natural' else 'Jurídico' end as ReasonSocial,
(select top 1 Addresss from Common.[Address] where IdPerson = tp.PersonId) as Addresss,
(select top 1 Phone from Common.[Phone] where IdPerson = tp.PersonId) as Phone, 
TiebackTax as ValueOne, 
mag.TotalRetenido as ValueTwo,
(TiebackTax - mag.TotalRetenido) as DifferenceValue

from Taxes.SingleTaxReturnForInsdutry as stri
inner join (select IdentificationNumber, sum(TaxWithheld) as TotalRetenido from taxes.TaxesMagneticMediaDetail
 group by IdentificationNumber) as mag on stri.IdentificationNumber = mag.IdentificationNumber and TiebackTax <> mag.TotalRetenido
 inner join Common.ThirdParty tp on tp.Nit = stri.IdentificationNumber
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que cruza las declaraciones privadas del impuesto de industria y comercio (ICA) con los valores de retención reportados en medios magnéticos (información exógena DIAN), para identificar discrepancias entre el valor de ICA declarado por cada contribuyente y el total de retenciones retenidas según los medios magnéticos. Por cada tercero con diferencia detectada, expone su NIT, tipo de persona (natural o jurídica), dirección y teléfono de contacto, el valor declarado en la declaración privada de ICA (ValueOne), el total retenido según medios magnéticos (ValueTwo) y la diferencia entre ambos (DifferenceValue). Se usa para auditoría y conciliación tributaria del impuesto de industria y comercio, permitiendo detectar inconsistencias o descuadres entre lo declarado ante el municipio y lo reportado a la DIAN.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'VIEW', @level1name = N'ViewDiscountReteIcaAndPrivateDeclaration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'VIEW', @level1name = N'ViewDiscountReteIcaAndPrivateDeclaration';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Comparar el valor retenido declarado en la declaración privada de ICA contra el total reportado en medios magnéticos por tercero, exponiendo únicamente los casos con diferencia.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDiscountReteIcaAndPrivateDeclaration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir coincidencia entre IdentificationNumber de la declaración ICA y el NIT del tercero en Common.ThirdParty.; Debe existir al menos un detalle de medios magnéticos con el mismo IdentificationNumber para que el tercero aparezca.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDiscountReteIcaAndPrivateDeclaration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen terceros cuyo valor de TiebackTax difiere del total retenido reportado en medios magnéticos (TiebackTax <> TotalRetenido).; El total retenido se calcula agregando (SUM) TaxWithheld por NIT en TaxesMagneticMediaDetail.; Solo se incluyen terceros que existen en Common.ThirdParty con NIT coincidente al de la declaración.; Para dirección y teléfono se toma únicamente el primer registro (TOP 1) sin orden definido por persona.; DifferenceValue siempre se calcula como TiebackTax - TotalRetenido.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDiscountReteIcaAndPrivateDeclaration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Impuesto de Industria y Comercio (ICA); Retención en la fuente (ReteICA); Declaración privada; Medios magnéticos tributarios; Tercero (NIT); Persona natural / jurídica', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDiscountReteIcaAndPrivateDeclaration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Taxes.ViewDiscountReteIcaAndPrivateDeclaration: Cuando TiebackTax de la declaración ICA difiere del SUM(TaxWithheld) en medios magnéticos para el mismo NIT, se retorna fila con ValueOne (declarado), ValueTwo (retenido en medios magnéticos) y DifferenceValue (diferencia).', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDiscountReteIcaAndPrivateDeclaration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PersonType = 1 → Se clasifica el tercero como ''Natural'' else Se clasifica como ''Jurídico''', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDiscountReteIcaAndPrivateDeclaration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Taxes.SingleTaxReturnForInsdutry; taxes.TaxesMagneticMediaDetail; Common.ThirdParty; Common.Address; Common.Phone', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDiscountReteIcaAndPrivateDeclaration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewDiscountReteIcaAndPrivateDeclaration';
GO
