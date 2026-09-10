-- =============================================
-- Author:		Cesar Collazos
-- Create date: 2026-05-29
-- Description:	Genera los datos del archivo FUR RG (Respuesta a Glosa)
--				conforme a la Circular Externa 003 de 2026 de ADRES.
--
--				Recibe el Id del radicado de objeciones y devuelve UN solo
--				result set con cabecera FUR + datos RG repetidos por cada
--				item glosado (LEFT JOIN cabecera ↔ glosas)
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateAdresFurRgData]
	@XmlParameters AS XML,
	@XmlInvoices AS XML
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY

		/*************************************** CRITERIOS ***************************************/
		-- @XmlParameters contiene <Data><ObjectionsReceptionId>X</ObjectionsReceptionId></Data>
		-- @XmlInvoices (opcional) contiene <Data><InvoiceNumber>FEV123</InvoiceNumber>...</Data>
		-- para restringir la generación a un subconjunto de facturas del radicado
		-- (caso multi-select desde el menú contextual). Cuando viene NULL o vacío,
		-- se procesan TODAS las facturas elegibles del radicado (caso botón superior).
		DECLARE @ObjectionsReceptionId INT

		SELECT @ObjectionsReceptionId = t.x.value('ObjectionsReceptionId[1]', 'int')
		FROM @XmlParameters.nodes('/Data') t(x)

		IF ISNULL(@ObjectionsReceptionId, 0) = 0
		BEGIN
			RAISERROR('No se recibió ObjectionsReceptionId en @XmlParameters.', 16, 1);
			RETURN;
		END;

		IF NOT EXISTS (
			SELECT 1 FROM Glosas.GlosaObjectionsReceptionC
			WHERE Id = @ObjectionsReceptionId
		)
		BEGIN
			RAISERROR('El radicado de objeciones no existe.', 16, 1);
			RETURN;
		END;

		-- Lista opcional de InvoiceNumber para filtrar (selección parcial)
		DECLARE @InvoiceFilter TABLE (InvoiceNumber VARCHAR(50) PRIMARY KEY)

		IF @XmlInvoices IS NOT NULL
		BEGIN
			INSERT INTO @InvoiceFilter (InvoiceNumber)
			SELECT DISTINCT LTRIM(RTRIM(t.x.value('InvoiceNumber[1]', 'varchar(50)')))
			FROM @XmlInvoices.nodes('/Data') t(x)
			WHERE t.x.value('InvoiceNumber[1]', 'varchar(50)') IS NOT NULL
		END

		DECLARE @HasInvoiceFilter BIT = CASE WHEN EXISTS (SELECT 1 FROM @InvoiceFilter) THEN 1 ELSE 0 END

		/*************************************** FACTURAS ELEGIBLES ***************************************/
		-- Facturas del radicado de objeciones cuyo grupo de atención es Aseguradora/Fosyga
		-- y cuyo estado de glosa indica que el trámite de Evaluación + Coordinación está
		-- completo (GlosaPortfolioGlosada.State IN (11=Glosa con Respuesta,
		-- 12=Reiteración con Respuesta)).
		DECLARE @EligibleInvoices TABLE
		(
			ObjectionsReceptionDId	INT,
			InvoiceId				INT,
			InvoiceNumber			VARCHAR(50),
			ADRESCode				VARCHAR(50)
		)

		INSERT INTO @EligibleInvoices (ObjectionsReceptionDId, InvoiceId, InvoiceNumber, ADRESCode)
		SELECT DISTINCT
			orD.Id,
			inv.Id,
			orD.InvoiceNumber,
			orD.ADRESCode
		FROM Glosas.GlosaObjectionsReceptionD orD
		INNER JOIN Glosas.GlosaPortfolioGlosada gpg
			ON gpg.Id = orD.PortfolioGlosaId
		INNER JOIN Billing.Invoice inv
			ON inv.InvoiceNumber = gpg.InvoiceNumber AND inv.Status = 1
		INNER JOIN Contract.CareGroup cg
			ON cg.Id = inv.CareGroupId
		WHERE orD.GlosaObjectionsReceptionCId = @ObjectionsReceptionId
		  AND cg.CareGroupType = 4		-- Aseguradoras
		  AND cg.EntityType    = 11		-- Fosyga
		  AND gpg.State IN (11, 12)		-- 11=Glosa con Respuesta, 12=Reiteración con Respuesta
		  AND (@HasInvoiceFilter = 0 OR orD.InvoiceNumber IN (SELECT InvoiceNumber FROM @InvoiceFilter))

		/*************************************** CTEs DE APOYO ***************************************/
		;WITH FurData AS
		(
			-- =============================================================================
			-- dbo.ADFURIPS (Circular 003 ADRES - EHR) — única fuente de cabecera FUR.
			-- Se toma el registro confirmado más reciente por factura (StatusTo = 1).
			-- =============================================================================
			SELECT
				ei.InvoiceId												AS InvoiceId,

				-- Datos víctima (subset FUR RG)
				LEFT(LTRIM(RTRIM(f.PatientAddress)), 100)					AS Direccion_residencia_victima,
				LTRIM(RTRIM(f.PatientPhone))								AS Telefono_victima,

				-- Datos sitio evento (subset FUR RG)
				LTRIM(RTRIM(f.VictimCondition))								AS Condicion_victima,
				CONVERT(VARCHAR(10), f.EventDate, 23)						AS Fecha_de_ocurrencia_evento,
				LTRIM(RTRIM(f.EventZone))									AS Zona_de_ocurrencia_evento,
				LTRIM(RTRIM(f.EventCity))									AS Codigo_municipio_ocurrencia_evento,
				LEFT(LTRIM(RTRIM(f.EventAddress)), 100)						AS Direccion_de_ocurrencia_evento,
				REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(f.EventShortDescription)), ',', ' '), '"', ' '), '    ', ' ')
																			AS Descripcion_corta_de_lo_ocurrido_en_el_evento,

				-- Datos vehículo (solo cuando EventNature = '01')
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.InsuranceStatus))			END	AS Estado_de_aseguramiento,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.VehiclePlateNumber))		END	AS Placa_vehiculo,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.VehicleType))				END	AS Tipo_de_Vehiculo,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.InsurerCode))				END	AS Codigo_de_la_aseguradora,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.SoatPolicyNumber))		END	AS Numero_de_poliza_SOAT,
				CASE WHEN f.EventNature = '01' THEN CONVERT(VARCHAR(10), f.PolicyStartDate, 23)	END	AS Fecha_de_inicio_de_vigencia_de_la_poliza,
				CASE WHEN f.EventNature = '01' THEN CONVERT(VARCHAR(10), f.PolicyEndDate, 23)	END	AS Fecha_final_de_vigencia_de_la_poliza,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.SirasNumber))				END	AS Numero_de_radicado_SIRAS,
				CASE WHEN f.EventNature = '01' THEN
					CASE f.InsurerCeilingCharge WHEN 1 THEN 'SI' WHEN 0 THEN 'NO' ELSE NULL END
				END															AS Cobro_por_agotamiento_tope_Aseguradora,

				-- Datos propietario (solo cuando EventNature = '01')
				CASE WHEN f.EventNature = '01' THEN dbo.TipDocR256(TRY_CAST(f.OwnerDocType AS INT))	END	AS Tipo_de_documento_de_identidad_del_propietario,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.OwnerDocNumber))			END	AS Numero_de_documento_de_identidad_del_propietario,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.OwnerFirstName))			END	AS Primer_nombre_del_propietario_o_razon_social,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.OwnerSecondName))			END	AS Segundo_nombre_del_propietario,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.OwnerFirstLastName))		END	AS Primer_apellido_del_propietario,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.OwnerSecondLastName))		END	AS Segundo_apellido_del_propietario,
				CASE WHEN f.EventNature = '01' THEN LEFT(LTRIM(RTRIM(f.OwnerAddress)), 100)	END	AS Direccion_de_residencia_del_propietario,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.OwnerPhone))				END	AS Telefono_de_residencia_del_propietario,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.OwnerCity))				END	AS Codigo_del_municipio_de_residencia_del_propietario,

				-- Datos conductor (solo cuando EventNature = '01')
				CASE WHEN f.EventNature = '01' THEN dbo.TipDocR256(TRY_CAST(f.DriverDocType AS INT)) END AS Tipo_de_documento_de_identidad_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.DriverDocNumber))			END	AS Numero_de_documento_de_identidad_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.DriverFirstName))			END	AS Primer_nombre_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.DriverSecondName))		END	AS Segundo_nombre_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.DriverFirstLastName))		END	AS Primer_apellido_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.DriverSecondLastName))	END	AS Segundo_apellido_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.DriverCity))				END	AS Codigo_del_municipio_de_residencia_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LEFT(LTRIM(RTRIM(f.DriverAddress)), 100) END AS Direccion_de_residencia_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.DriverPhone))				END	AS Telefono_de_residencia_del_conductor,

				-- Datos atención víctima
				CASE f.OsteosynthesisMaterial WHEN 1 THEN 'SI' WHEN 0 THEN 'NO' ELSE NULL END	AS Uso_material_de_osteosintesis_en_la_atencion,
				LTRIM(RTRIM(f.AttendanceType))								AS Es_atencion_inicial_paciente_remitido_o_control,

				-- Datos remisión / transporte / movilización
				LTRIM(RTRIM(f.SecondaryTransportPlate))						AS Placa_ambulancia_que_realiza_la_remision,
				LTRIM(RTRIM(f.SecondaryTransportPlate))						AS Placa_ambulancia_que_realiza_el_traslado_secundario,
				LTRIM(RTRIM(f.ReferringProviderCode))						AS Codigo_de_habilitacion_del_prestador_que_remite,
				dbo.TipDocR256(TRY_CAST(f.ReceivingProfessionalDocType AS INT))	AS TIPO_de_documento_Profesional_que_recibe,
				LTRIM(RTRIM(f.ReceivingProfessionalDocNumber))				AS Numero_de_documento_Profesional_que_recibe,
				LTRIM(RTRIM(f.ReceivingProviderCode))						AS Codigo_de_habilitacion_del_prestador_que_recibe,
				CONVERT(VARCHAR(10), f.AcceptanceDate, 23)					AS Fecha_de_aceptacion,
				LEFT(CONVERT(VARCHAR(8), f.AcceptanceTime, 108), 5)			AS Hora_aceptacion,
				LTRIM(RTRIM(f.PrimaryTransportPlate))						AS Placa_ambulancia_que_realiza_el_traslado,
				LEFT(LTRIM(RTRIM(f.EventSiteAddress)), 100)					AS Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion,
				LEFT(LTRIM(RTRIM(f.DestinationIpsAddress)), 100)			AS Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS
			FROM @EligibleInvoices ei
			JOIN (
				SELECT InvoiceId, MAX(ADFURIPSId) AS ADFURIPSId
				FROM dbo.ADFURIPSInvoice
				WHERE StatusTo = 1
				GROUP BY InvoiceId
			) fi ON fi.InvoiceId = ei.InvoiceId
			JOIN dbo.ADFURIPS f ON fi.ADFURIPSId = f.Id
		),
		-- Valor pendiente por conciliar por factura (suma sobre todas las glosas)
		PendingByInvoice AS
		(
			SELECT
				gid.ObjectionsReceptionDId,
				SUM(ISNULL(gmg.ValuePendingConciliation, 0)) AS ValorReclamado
			FROM Glosas.GlosaMovementGlosa gmg
			JOIN Glosas.GlosaInvoiceDetail gid ON gid.Id = gmg.InvoiceDetailId
			JOIN @EligibleInvoices ei ON ei.ObjectionsReceptionDId = gid.ObjectionsReceptionDId
			GROUP BY gid.ObjectionsReceptionDId
		),
		-- Radicado cliente más reciente confirmado por factura
		RadicateClientByInvoice AS
		(
			SELECT
				rid.InvoiceNumber,
				ric.CustomerRadicateConsecutive,
				ROW_NUMBER() OVER (PARTITION BY rid.InvoiceNumber ORDER BY ISNULL(ric.ConfirmDate, ric.RadicatedDate) DESC, ric.Id DESC) AS rn
			FROM Portfolio.RadicateInvoiceC ric
			JOIN Portfolio.RadicateInvoiceD rid ON rid.RadicateInvoiceCId = ric.Id
			WHERE ric.State = '2'
			  AND rid.State <> '4'
		),
		-- Nota Crédito más reciente por factura
		CreditNoteByInvoice AS
		(
			SELECT
				bn.EntityId AS InvoiceId,
				bn.Code,
				ROW_NUMBER() OVER (PARTITION BY bn.EntityId ORDER BY bn.NoteDate DESC, bn.Id DESC) AS rn
			FROM Billing.BillingNote bn
			WHERE bn.Nature = 2
		)
		/*************************************** RETURN ***************************************/

		SELECT
			ei.InvoiceId,
			(SELECT TOP 1 LTRIM(RTRIM(INDNUMIDE)) FROM dbo.INEMPRESU)		AS NIT_PRESTADOR,
			LTRIM(RTRIM(ei.InvoiceNumber))									AS NUM_FACTURA,
			CAST(NULL AS VARCHAR(50))										AS Num_factura_anterior,
			LTRIM(RTRIM(rc.CustomerRadicateConsecutive))					AS Numero_radicacion,
			LTRIM(RTRIM(cn.Code))											AS CreditNote_o_Debit_note,
			ISNULL(pi.ValorReclamado, 0)									AS Valor_reclamado,

			-- ============ Bloque FUR (puede venir NULL si no hay FUR confirmado) ============
			fd.Direccion_residencia_victima,
			fd.Telefono_victima,
			fd.Condicion_victima,
			fd.Fecha_de_ocurrencia_evento,
			fd.Zona_de_ocurrencia_evento,
			fd.Codigo_municipio_ocurrencia_evento,
			fd.Direccion_de_ocurrencia_evento,
			fd.Descripcion_corta_de_lo_ocurrido_en_el_evento,
			fd.Estado_de_aseguramiento,
			fd.Placa_vehiculo,
			fd.Tipo_de_Vehiculo,
			fd.Codigo_de_la_aseguradora,
			fd.Numero_de_poliza_SOAT,
			fd.Fecha_de_inicio_de_vigencia_de_la_poliza,
			fd.Fecha_final_de_vigencia_de_la_poliza,
			fd.Numero_de_radicado_SIRAS,
			fd.Cobro_por_agotamiento_tope_Aseguradora,
			fd.Tipo_de_documento_de_identidad_del_propietario,
			fd.Numero_de_documento_de_identidad_del_propietario,
			fd.Primer_nombre_del_propietario_o_razon_social,
			fd.Segundo_nombre_del_propietario,
			fd.Primer_apellido_del_propietario,
			fd.Segundo_apellido_del_propietario,
			fd.Direccion_de_residencia_del_propietario,
			fd.Telefono_de_residencia_del_propietario,
			fd.Codigo_del_municipio_de_residencia_del_propietario,
			fd.Tipo_de_documento_de_identidad_del_conductor,
			fd.Numero_de_documento_de_identidad_del_conductor,
			fd.Primer_nombre_del_conductor,
			fd.Segundo_nombre_del_conductor,
			fd.Primer_apellido_del_conductor,
			fd.Segundo_apellido_del_conductor,
			fd.Codigo_del_municipio_de_residencia_del_conductor,
			fd.Direccion_de_residencia_del_conductor,
			fd.Telefono_de_residencia_del_conductor,
			fd.Uso_material_de_osteosintesis_en_la_atencion,
			fd.Es_atencion_inicial_paciente_remitido_o_control,
			fd.Placa_ambulancia_que_realiza_la_remision,
			fd.Placa_ambulancia_que_realiza_el_traslado_secundario,
			fd.Codigo_de_habilitacion_del_prestador_que_remite,
			fd.TIPO_de_documento_Profesional_que_recibe,
			fd.Numero_de_documento_Profesional_que_recibe,
			fd.Codigo_de_habilitacion_del_prestador_que_recibe,
			fd.Fecha_de_aceptacion,
			fd.Hora_aceptacion,
			fd.Placa_ambulancia_que_realiza_el_traslado,
			fd.Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion,
			fd.Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS,

			-- ============ Bloque GLOSA (una fila por item glosado; NULL si la factura no tiene glosas) ============
			gmg.Id															AS GlosaId,
			LTRIM(RTRIM(ei.ADRESCode))										AS ID_interno_Glosa,
			LTRIM(RTRIM(ISNULL(gidQX.ServiceCode, gid.ServiceCode)))		AS itemID_servicio_o_tecnologia_objetado,
			LTRIM(RTRIM(cgGlosa.Code))										AS Codigo_glosa,
			LEFT(LTRIM(RTRIM(cgEval.Code)), 4)								AS Tipo_respuesta_a_glosa,
			REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(gmg.JustificationGlosaText)), CHAR(13), ' '), CHAR(10), ' '), '"', ' ')
																			AS Respuesta_a_glosa,
			CASE
				WHEN cn.Code IS NULL								THEN NULL
				WHEN gmg.ValueAcceptedIPSconciliation IS NULL		THEN NULL
				ELSE gid.Ammount
			END																AS Cantidad_aceptada,
			gmg.ValueAcceptedIPSconciliation								AS Valor_aceptado,
			CASE
				WHEN p.FirstName IS NULL AND p.FirstLastName IS NULL	THEN NULL
				ELSE LTRIM(RTRIM(ISNULL(p.FirstName, '') + ' ' + ISNULL(p.FirstLastName, '')))
			END																AS Primer_nombre_primer_apellido_auditor,
			LTRIM(RTRIM(u.Position))										AS Perfil_auditor

		FROM @EligibleInvoices ei
		LEFT JOIN FurData fd					ON fd.InvoiceId = ei.InvoiceId
		LEFT JOIN PendingByInvoice pi			ON pi.ObjectionsReceptionDId = ei.ObjectionsReceptionDId
		LEFT JOIN RadicateClientByInvoice rc	ON rc.InvoiceNumber = ei.InvoiceNumber AND rc.rn = 1
		LEFT JOIN CreditNoteByInvoice cn		ON cn.InvoiceId = ei.InvoiceId AND cn.rn = 1
		LEFT JOIN Glosas.GlosaInvoiceDetail gid	ON gid.ObjectionsReceptionDId = ei.ObjectionsReceptionDId
		LEFT JOIN Glosas.GlosaMovementGlosa gmg	ON gmg.InvoiceDetailId = gid.Id
		LEFT JOIN Glosas.GlosaInvoiceDetailQX gidQX	ON gidQX.Id = gmg.InvoiceDetailIdQX
		LEFT JOIN Common.ConceptGlosas cgGlosa	ON cgGlosa.Id = gmg.CodeGlosaId
		LEFT JOIN Common.ConceptGlosas cgEval	ON cgEval.Id = gmg.IdGlosaEvaluation
		LEFT JOIN Glosas.ConciliationC cc		ON cc.Id = gmg.ConciliationCId
		LEFT JOIN [Security].[User] u			ON u.Id = cc.ConfirmUser
		LEFT JOIN Common.Person p				ON p.Id = u.IdPerson
		ORDER BY ei.InvoiceNumber, gmg.Id

	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
	END CATCH
END
