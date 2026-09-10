
CREATE   PROCEDURE [dbo].[SP_ADM_ExportarDatosFur]
    @CodigoFur VARCHAR(20),
    @PatienteCode varchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1 FROM dbo.ADFURIPS 
        WHERE FurCode = @CodigoFur AND ConfirmationStatus = 1
    )
    BEGIN
        RAISERROR('El FUR no existe o no se encuentra en estado Confirmado.', 16, 1);
        RETURN;
    END;

    SELECT
        (SELECT TOP 1 INDNUMIDE FROM INEMPRESU)                             AS NIT_PRESTADOR,
        LTRIM(RTRIM(fac.InvoiceNumber))                                     AS NUM_FACTURA,

        -- Tipos de documento: se exporta la SIGLA
        (SELECT TOP 1 SIGLA FROM ADTIPOIDENTIFICA WHERE CODIGO = f.PatientDocType)
                                                                            AS Tipo_documento_identidad_victima,
        f.PatientCode                                                       AS Numero_documento_identidad_victima,

        CASE
            WHEN f.SpecialPopulation = 1 THEN f.SpecialPopulationType
            ELSE NULL
        END                                                                 AS Tipo_de_poblacion_especial,

        f.PatientFirstName                                                  AS Primer_nombre_victima,
        f.PatientSecondName                                                 AS Segundo_nombre_victima,
        f.PatientFirstLastName                                              AS Primer_apellido_victima,
        f.PatientSecondLastName                                             AS Segundo_apellido_victima,
        f.PatientAddress                                                    AS Direccion_residencia_victima,
        LTRIM(RTRIM( isnull(u.DEPMUNCOD,f.PatientCityCode)))                AS Codigo_municipio_residencia_victima,
        f.PatientPhone                                                      AS Telefono_victima,
        f.EventNature                                                       AS Naturaleza_del_evento,
        f.OtherEventDescription                                             AS Descripcion_del_otro_evento,
        f.VictimCondition                                                   AS Condicion_victima,
        CONVERT(VARCHAR(10), f.EventDate, 23)                               AS Fecha_de_ocurrencia_evento,
        f.EventZone                                                         AS Zona_de_ocurrencia_evento,

        CASE
            WHEN f.EventDepartment IS NOT NULL AND f.EventCity IS NOT NULL
            THEN f.EventDepartment + f.EventCity
            ELSE NULL
        END                                                                 AS Codigo_municipio_ocurrencia_evento,

        f.EventAddress                                                      AS Direccion_de_ocurrencia_evento,
        f.EventShortDescription                                             AS Descripcion_corta_de_lo_ocurrido_en_el_evento,
        f.InsuranceStatus                                                   AS Estado_de_aseguramiento,
        f.VehiclePlateNumber                                                AS Placa_vehiculo,
        f.VehicleType                                                       AS Tipo_de_Vehiculo,
        f.InsurerCode                                                       AS Codigo_de_la_aseguradora,
        f.SoatPolicyNumber                                                  AS Numero_de_poliza_SOAT,
        CONVERT(VARCHAR(10), f.PolicyStartDate, 23)                         AS Fecha_de_inicio_de_vigencia_de_la_poliza,
        CONVERT(VARCHAR(10), f.PolicyEndDate, 23)                           AS Fecha_final_de_vigencia_de_la_poliza,
        f.SirasNumber                                                       AS Numero_de_radicado_SIRAS,
        CAST(f.InsurerCeilingCharge AS TINYINT)                             AS Cobro_por_agotamiento_tope_Aseguradora,

        -- Tipo documento propietario: SIGLA
        (SELECT TOP 1 SIGLA FROM ADTIPOIDENTIFICA WHERE CODIGO = f.OwnerDocType)
                                                                            AS Tipo_de_documento_de_identidad_del_propietario,
        f.OwnerDocNumber                                                    AS Numero_de_documento_de_identidad_del_propietario,
        f.OwnerFirstName                                                    AS Primer_nombre_del_propietario_o_razon_social,
        f.OwnerSecondName                                                   AS Segundo_nombre_del_propietario,
        f.OwnerFirstLastName                                                AS Primer_apellido_del_propietario,
        f.OwnerSecondLastName                                               AS Segundo_apellido_del_propietario,
        f.OwnerAddress                                                      AS Direccion_de_residencia_del_propietario,
        f.OwnerPhone                                                        AS Telefono_de_residencia_del_propietario,

        CASE
            WHEN f.OwnerDepartment IS NOT NULL AND f.OwnerCity IS NOT NULL
            THEN f.OwnerDepartment + f.OwnerCity
            ELSE NULL
        END                                                                 AS Codigo_del_municipio_de_residencia_del_propietario,

        -- Tipo documento conductor: SIGLA
        (SELECT TOP 1 SIGLA FROM ADTIPOIDENTIFICA WHERE CODIGO = f.DriverDocType)
                                                                            AS Tipo_de_documento_de_identidad_del_conductor,
        f.DriverDocNumber                                                   AS Numero_de_documento_de_identidad_del_conductor,
        f.DriverFirstName                                                   AS Primer_nombre_del_conductor,
        f.DriverSecondName                                                  AS Segundo_nombre_del_conductor,
        f.DriverFirstLastName                                               AS Primer_apellido_del_conductor,
        f.DriverSecondLastName                                              AS Segundo_apellido_del_conductor,

        CASE
            WHEN f.DriverDepartment IS NOT NULL AND f.DriverCity IS NOT NULL
            THEN f.DriverDepartment + f.DriverCity
            ELSE NULL
        END                                                                 AS Codigo_del_municipio_de_residencia_del_conductor,

        f.DriverAddress                                                     AS Direccion_de_residencia_del_conductor,
        f.DriverPhone                                                       AS Telefono_de_residencia_del_conductor,
        CAST( CASE WHEN f.OsteosynthesisMaterial = 1 THEN 1 WHEN f.OsteosynthesisMaterial = 0 THEN 2  end AS TINYINT)                   AS Uso_material_de_osteosintesis_en_la_atencion,
        f.AttendanceType                                                    AS Es_atencion_inicial_paciente_remitido_o_control,
        f.SecondaryTransportPlate                                           AS Placa_ambulancia_que_realiza_el_traslado_secundario,
        NULLIF(f.SecondaryTransportType,0)                                  AS Tipo_de_servicio_del_transporte_secundario,
        f.ReferringProviderCode                                             AS Codigo_de_habilitacion_del_prestador_que_remite,
        f.ReceivingProviderCode                                             AS Codigo_de_habilitacion_del_prestador_que_recibe,

        -- Tipo documento profesional que recibe: SIGLA
        (SELECT TOP 1 SIGLA FROM ADTIPOIDENTIFICA WHERE CODIGO = f.ReceivingProfessionalDocType)
                                                                            AS TIPO_de_documento_Profesional_que_recibe,

        f.ReceivingProfessionalDocNumber                                    AS Numero_de_documento_Profesional_que_recibe,
        CONVERT(VARCHAR(10), f.AcceptanceDate, 23)                          AS Fecha_de_aceptacion,
        LEFT(CONVERT(VARCHAR(8), f.AcceptanceTime, 108), 5)                 AS Hora_aceptacion,
        NULLIF(f.PrimaryTransportType, 0)                                   AS Tipo_de_servicio_del_transporte,
        f.PrimaryTransportPlate                                             AS Placa_ambulancia_que_realiza_el_traslado,
        f.ReceptorProviderCode                                              AS Codigo_de_habilitacion_del_prestador_que_recibe_transporte_primario,
        f.EventSiteAddress                                                  AS Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion,
        f.DestinationIpsAddress                                             AS Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS

    FROM dbo.ADFURIPS f
    INNER JOIN Billing.Invoice Fac ON Fac.Id = f.InvoiceNumber
	INNER JOIN ADINGRESO AI ON AI.NUMINGRES = Fac.AdmissionNumber
	INNER JOIN dbo.INPACIENT a ON A.IPCODPACI = AI.IPCODPACI
	LEFT JOIN .dbo.INUBICACI u ON u.AUUBICACI = a.AUUBICACI

    WHERE f.FurCode = @CodigoFur AND f.PatientCode=@PatienteCode AND f.ConfirmationStatus = 1;

END
GO


