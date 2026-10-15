

CREATE PROCEDURE [Admissions].[SP_ListReportFURTRAN]
(
  @Codigo as varchar(25),
  @Ingreso as char(10) 
)

AS
BEGIN
  SET NOCOUNT ON;
		
		Select
			--============================= I. Datos del transportador reclamante ============================================
			Format(A.FilingDate, 'dd/MM/yyy') as Fecha_radicacion, A.PreviousFilingNumber as Numero_radicado_anterior,
			case A.GlossyResponse when 1 then 'Glosa u objeción total' when 2 then 'Pago parcial' when 3 then 'Glosa transversal' end as RG,

			A.InvoiceNumber as Numero_factura, I.CODHABILITA as Codigo_habilitacion, A.DriverFirstName as C_Primer_Nombre,
			A.DriverSecondName as C_Segundo_Nombre, A.DriverFirstLastName as C_Primer_Apellido, A.DriverSecondLastName as C_Segundo_Apellido,
			J.SIGLA as C_Tipo_Documento, A.DriverIdentificationNumber as C_Numero_Documento,
			
			case A.DriverVehicleType when 1 then 'X' end as Ambulancia_basica,
			case A.DriverVehicleType when 2 then 'X' end as Ambulancia_medicalizada,
			case A.DriverVehicleType when 3 then 'X' end as Particular,
			case A.DriverVehicleType when 4 then 'X' end as Publico,
			case A.DriverVehicleType when 5 then 'X' end as Oficial,
			case A.DriverVehicleType when 6 then 'X' end as De_emergencia,
			case A.DriverVehicleType when 7 then 'X' end as Diplomatico_consular,
			case A.DriverVehicleType when 8 then 'X' end as Transporte_masivo,
			case A.DriverVehicleType when 9 then 'X' end as Escolar,
			
			A.DriverVehiclePlate as C_Placa_vehiculo, A.DriverAddress as C_Direccion, A.DriverPhoneNumber as C_Telefono, B.nomdepart as C_Departamento,
			C.MUNNOMBRE as C_Municipio,

			--============================== II. Datos de la víctima trasladada ===============================================
			K.SIGLA as V_Tipo_Documento, D.IPCODPACI as V_Numero_Documento, D.IPPRIAPEL as V_Primer_Apellido, D.IPSEGAPEL as V_Segundo_Apellido, 
			D.IPPRINOMB as V_Primer_Nombre, D.IPSEGNOMB as V_Segundo_Nombre, Format(D.IPFECNACI, 'dd/MM/yyy') as V_Fecha_Nacimiento, 
			case A.VictimSex when 1 then 'Masculino' when 2 then 'Femenino' when 3 then 'Otro' end as V_Sexo,

			--============================== III. Identificaón del tipo del evento ============================================
			case A.EvenType when 1 then 'X' end as Accidente_Transito,
			case A.EvenType when 2 then 'X' end as Evento_Catastrofico,
			case A.EvenType when 3 then 'X' end as Evento_Terrorista,

			--============================== IV. Lugar en el que se recoge la víctima =========================================
			A.VictimPlaceAddress as V_Direccion, E.nomdepart as V_Departamento, F.MUNNOMBRE as V_Municipio,
			case A.VictimPlaceZone when 1 then 'X' end as  V_Urbana, case A.VictimPlaceZone when 0 then 'X' end as  V_Rural, 
			
			--============================== V. Certificación de traslado de víctimas =========================================
			format(A.VictimCertificateTransferDate,'dd/MM/yyy') as Fecha_Traslado, FORMAT(A.VictimCertificateTransferTime, '') as Hora_traslado,
			M.CODHABILITA as Codigo_Ips, A.VictimCertificateAddress as Direccion_Traslado, A.VictimCertificatePhoneNumber as Telefono_traslado,
			G.nomdepart as Departamento_Traslado, H.MUNNOMBRE as Municipio_Traslado,

			--======================= VI. Datos obligatorios si el evento es un accidente de tránsito =========================
			case A.VictimCondition when 1 then 'X' end as Conductor,
			case A.VictimCondition when 2 then 'X' end as Peaton,
			case A.VictimCondition when 3 then 'X' end as Ocupante,
			case A.VictimCondition when 4 then 'X' end as Ciclista,

			case A.VictimAssuranceStatus when 1 then 'X' end as Asegurado,
			case A.VictimAssuranceStatus when 2 then 'X' end as No_asegurado,
			case A.VictimAssuranceStatus when 3 then 'X' end as V_fantasma, 
			case A.VictimAssuranceStatus when 4 then 'X' end as Poliza_falsa,
			case A.VictimAssuranceStatus when 5 then 'X' end as No_asegurado_PISI,
			case A.VictimAssuranceStatus when 6 then 'X' end as  N_asegurado_Sin_placa,

			case A.VictimVehicleType when 1 then 'X' end as Automovil,
			case A.VictimVehicleType when 2 then 'X' end as Bus,
			case A.VictimVehicleType when 3 then 'X' end as Buseta,
			case A.VictimVehicleType when 4 then 'X' end as Camion,
			case A.VictimVehicleType when 5 then 'X' end as Camioneta,
			case A.VictimVehicleType when 6 then 'X' end as Campero,
			case A.VictimVehicleType when 7 then 'X' end as Microbus,
			case A.VictimVehicleType when 8 then 'X' end as Tractocamion,
			case A.VictimVehicleType when 9 then 'X' end as Motocicleta,
			case A.VictimVehicleType when 10 then 'X' end as Motocarro,
			case A.VictimVehicleType when 11 then 'X' end as Moto_triciclo,
			case A.VictimVehicleType when 12 then 'X' end as Cuatrimoto, 
			case A.VictimVehicleType when 13 then 'X' end as Moto_extranjera,
			case A.VictimVehicleType when 14 then 'X' end as Vehiculo_extranjero,
			case A.VictimVehicleType when 15 then 'X' end as Volqueta,

			A.VictimVehiclePlate as Placa_VehiculoI, A.VictimInsuranceCode as Cod_Aseguradora, A.VictimPolicyNumber as Num_Poliza,
			Format(A.VictimPolicyStartDate,'dd/MM/yyy') as Fecha_PolizaI, Format(A.VictimPolicyEndDate, 'dd/MM/yyy') as Fecha_PolizaF, 
			A.VictimSIRASFilingNumber as Num_SIRAS,

			--======================================== VII. Amparo reclamado ==================================================
			A.InvoicedValue as Valor_facturado, A.ClaimedValue as Valor_reclamado,

			--============== VIII. Manifestación del servicio habilitado del prestador de servicios de salud ==================
			Case A.ManifestationOfEnabledServices when 1 then 'X' end as MSH_Si,
			Case A.ManifestationOfEnabledServices when 0 then 'X' end as MSH_No

			from Admissions.Furtran A
				
				inner join INDEPARTA B on A.DriverDepartmentCode = B.depcodigo
				inner join INMUNICIP C on A.DriverMunicipalityCode = C.DEPMUNCOD
				inner join INPACIENT D on A.PatientCode = D.IPCODPACI
				inner join INDEPARTA E on A.VictimPlaceDepartmentCode = E.depcodigo
				inner join INMUNICIP F on A.VictimPlaceMunicipalityCode = F.DEPMUNCOD
				inner join INDEPARTA G on A.VictimCertificateDepartmentCode = G.depcodigo
				inner join INMUNICIP H on A.VictimCertificateMunicipalityCode = H.DEPMUNCOD
				inner join ADCONTIPS I on A.IPSCode = I.CODIGOIPS
				inner join ADTIPOIDENTIFICA J on A.DriverIdentificationType = J.CODIGO
				inner join ADTIPOIDENTIFICA K on D.IPTIPODOC = K.CODIGO
				inner join ADCONTIPS M on A.VictimCertificateIPS = M.CODIGOIPS
			Where 
				A.PatientCode  = @Codigo and A.EntryNumber = @Ingreso

END