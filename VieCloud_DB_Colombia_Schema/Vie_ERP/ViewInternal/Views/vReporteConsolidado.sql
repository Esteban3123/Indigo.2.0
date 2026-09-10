

CREATE view [ViewInternal].[vReporteConsolidado]
as (
SELECT        i.InvoiceDate AS ff, ad.IPCODPACI AS Cedula, pa.IPNOMCOMP AS Nombre, ad.NUMINGRES AS Ingreso, ic.Code + ' - ' + ic.Name as Categoria, cg.Code + ' - ' + cg.Name AS GrupoAtencion, 
                         isnull(ha.Code + ' - ','') + cg.Name AS EntidadAdministradora,
						 case
						  when cg.EntityType ='1' then 'EPS Contributivo' 
						  when cg.EntityType = '2' then  'EPS Subsidiado' 
						  when cg.EntityType = '3' then 'ET Vinculados Municipios'
						  when cg.EntityType = '4' then 'ET Vinculados Departamentos' 
						  when cg.EntityType = '5'  then 'ARL Riesgos Laborales' 
						  when cg.EntityType = '6' then 'MP Medicina Prepagada' 
						  when cg.EntityType = '7'  then 'IPS Privada' 
						  when cg.EntityType = '8'  then 'IPS Publica' 
						  when cg.EntityType = '9'  then 'Regimen Especial' 
						  when cg.EntityType = '10'  then 'Accidentes de transito' 
						  when cg.EntityType = '11'  then 'Fosyga' 
						  when cg.EntityType = '12'  then 'Otros' 
						  when cg.EntityType = '13'  then 'Aseguradoras' 
						  when cg.EntityType = '99'  then 'Particulares'
						 end as Regimen,
						  isnull(ha.Name, cg.Name) AS Tercero, i.InvoiceNumber AS Factura, i.InvoicedUser AS UsuarioFacturacion, 
                         Pe.Fullname AS NomFacturador, cast(i.InvoiceDate as date) AS FechaFactura, i.TotalInvoice AS TotalFactura, i.ThirdPartySalesValue AS TotalEntidad, 
                         CASE WHEN i.Status = 1 THEN 'Facturado' WHEN i.Status = 2 THEN 'Anulado' END AS EstadoF, i.AnnulmentUser AS AnulaUsuario, 
                         i.AnnulmentDate AS AnulaFecha, Billing.BillingReversalReason.Name AS AnulaRazon, Billing.BillingReversalReason.Description AS AnulaDescripcion
FROM            Billing.Invoice AS i INNER JOIN
				Billing.InvoiceCategories ic on ic.Id = i.InvoiceCategoryId inner join
                         dbo.ADINGRESO AS ad ON ad.NUMINGRES = i.AdmissionNumber INNER JOIN
                         dbo.INPACIENT AS pa ON ad.IPCODPACI = pa.IPCODPACI INNER JOIN
					     Contract.CareGroup AS cg ON cg.Id = i.CareGroupId INNEr JOIN
                         Security.[User] AS u ON u.UserCode = i.InvoicedUser inner JOIN
						 Security.Person pe ON pe.Id = u.IdPerson left join
                         Contract.HealthAdministrator AS ha ON ha.Id = i.HealthAdministratorId left JOIN
                         Billing.BillingReversalReason ON i.ReversalReasonId = Billing.BillingReversalReason.Id
						 )
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte consolidado de facturación que cruza facturas emitidas con datos del paciente, admisión, categoría de factura, grupo de atención y entidad pagadora. Traduce el código numérico de tipo de entidad (`EntityType`) al régimen correspondiente en el sistema de salud colombiano (EPS, ARL, Fosyga, Particulares, etc.). Incluye información del usuario facturador, totales cobrados (a paciente y tercero), estado de la factura y, cuando aplica, datos de anulación con su motivo y descripción. Está orientada al consumo en reportes de gestión y auditoría de facturación.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vReporteConsolidado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vReporteConsolidado';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida un reporte de facturación cruzando facturas con admisión, paciente, grupo de atención, administradora de salud, usuario facturador y motivo de anulación, traduciendo códigos de régimen y estado a etiquetas legibles.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vReporteConsolidado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Toda factura debe tener admisión válida en ADINGRESO, paciente en INPACIENT, grupo de atención en CareGroup y usuario facturador con persona asociada (INNER JOIN obligatorios); La administradora de salud y el motivo de reversión son opcionales (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vReporteConsolidado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa una factura con su régimen y entidad administradora resueltos; El régimen se deriva exclusivamente del EntityType del grupo de atención; Si no hay administradora de salud asociada, el grupo de atención actúa como tercero pagador; Solo se exponen los estados ''Facturado'' y ''Anulado''; otros valores de Status quedan en NULL', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vReporteConsolidado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Paciente; Ingreso/Admisión; Grupo de atención; Entidad administradora (EPS, ARL, Medicina Prepagada, IPS, Fosyga, Aseguradoras, Particulares); Régimen de salud (Contributivo, Subsidiado, Vinculados, Especial); Anulación/Reversión de factura; Usuario facturador; Tercero pagador; Valor total factura y valor a cargo del tercero', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vReporteConsolidado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve un set consolidado de facturación por cada factura con sus relaciones de paciente, admisión, grupo de atención y entidad administradora', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vReporteConsolidado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EntityType del CareGroup → Se traduce el código numérico (1..13, 99) a la etiqueta de régimen correspondiente: Contributivo, Subsidiado, Vinculados, ARL, Medicina Prepagada, IPS Privada/Pública, Régimen Especial, Accidentes de Tránsito, Fosyga, Otros, Aseguradoras, Particulares else NULL (régimen no clasificado); si Status de la factura → 1=''Facturado'', 2=''Anulado'' else NULL; si HealthAdministrator existe (ha.Code no nulo) → EntidadAdministradora = Code + '' - '' + Name del grupo de atención; Tercero = Name de la administradora else EntidadAdministradora = solo Name del grupo de atención; Tercero = Name del grupo de atención', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vReporteConsolidado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceCategories; dbo.ADINGRESO; dbo.INPACIENT; Contract.CareGroup; Security.User; Security.Person; Contract.HealthAdministrator; Billing.BillingReversalReason', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vReporteConsolidado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vReporteConsolidado';
GO
