CREATE TABLE [dbo].[ADFURIPS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FurCode] [varchar](20) NOT NULL,
	[InvoiceNumber] [int] NULL,
	[PatientCode] [varchar](25) NOT NULL,
	[AdmissionNumber] [varchar](15) NOT NULL,
	[PatientDocType] [int] NULL,
	[SpecialPopulation] [bit] NOT NULL,
	[SpecialPopulationType] [varchar](5) NULL,
	[PatientFirstName] [varchar](50) NULL,
	[PatientSecondName] [varchar](50) NULL,
	[PatientFirstLastName] [varchar](50) NULL,
	[PatientSecondLastName] [varchar](50) NULL,
	[PatientAddress] [varchar](max) NULL,
	[PatientCityCode] [varchar](20) NULL,
	[PatientPhone] [varchar](20) NULL,
	[EventNature] [varchar](5) NOT NULL,
	[OtherEventDescription] [varchar](40) NULL,
	[VictimCondition] [varchar](5) NULL,
	[EventDate] [date] NULL,
	[EventZone] [varchar](5) NULL,
	[EventDepartment] [varchar](10) NULL,
	[EventCity] [varchar](10) NULL,
	[EventAddress] [varchar](100) NULL,
	[EventShortDescription] [varchar](1000) NULL,
	[InsuranceStatus] [varchar](5) NULL,
	[VehiclePlateNumber] [varchar](10) NULL,
	[VehicleType] [varchar](5) NULL,
	[InsurerCode] [varchar](6) NULL,
	[SoatPolicyNumber] [varchar](20) NULL,
	[PolicyStartDate] [date] NULL,
	[PolicyEndDate] [date] NULL,
	[SirasNumber] [varchar](20) NULL,
	[InsurerCeilingCharge] [bit] NULL,
	[OwnerDocType] [varchar](10) NULL,
	[OwnerDocNumber] [varchar](20) NULL,
	[OwnerFirstName] [varchar](50) NULL,
	[OwnerSecondName] [varchar](50) NULL,
	[OwnerFirstLastName] [varchar](50) NULL,
	[OwnerSecondLastName] [varchar](50) NULL,
	[OwnerAddress] [varchar](100) NULL,
	[OwnerPhone] [varchar](10) NULL,
	[OwnerDepartment] [varchar](10) NULL,
	[OwnerCity] [varchar](10) NULL,
	[DriverDocType] [varchar](10) NULL,
	[DriverDocNumber] [varchar](20) NULL,
	[DriverFirstName] [varchar](50) NULL,
	[DriverSecondName] [varchar](50) NULL,
	[DriverFirstLastName] [varchar](50) NULL,
	[DriverSecondLastName] [varchar](50) NULL,
	[DriverAddress] [varchar](100) NULL,
	[DriverPhone] [varchar](10) NULL,
	[DriverDepartment] [varchar](10) NULL,
	[DriverCity] [varchar](10) NULL,
	[OsteosynthesisMaterial] [bit] NULL,
	[AttendanceType] [varchar](5) NULL,
	[SecondaryTransportPlate] [varchar](7) NULL,
	[SecondaryTransportType] [varchar](5) NULL,
	[ReferringProviderCode] [varchar](12) NULL,
	[ReceivingProviderCode] [varchar](12) NULL,
	[ReceivingProfessionalDocType] [varchar](10) NULL,
	[ReceivingProfessionalDocNumber] [varchar](20) NULL,
	[AcceptanceDate] [date] NULL,
	[AcceptanceTime] [time](0) NULL,
	[PrimaryTransportType] [varchar](5) NULL,
	[PrimaryTransportPlate] [varchar](7) NULL,
	[ReceptorProviderCode] [varchar](12) NULL,
	[EventSiteAddress] [varchar](100) NULL,
	[DestinationIpsAddress] [varchar](100) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[ConfirmationStatus] [int] NULL,
	[ConfirmationUser] [varchar](50) NULL,
	[ConfirmationDate] [datetime] NULL,
 CONSTRAINT [PK_ADFURIPS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ADFURIPS] ADD  CONSTRAINT [DF_ADFURIPS_ConfirmationStatus]  DEFAULT ((0)) FOR [ConfirmationStatus]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador único autoincremental del formulario FUR' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código alfanumérico del FUR, máximo 20 caracteres' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'FurCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de factura asociada al FUR, máximo 20 caracteres' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'InvoiceNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del paciente relacionado al FUR' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'PatientCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de ingreso del paciente seleccionado en los filtros de consulta' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'AdmissionNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de documento de identificación del paciente, postulado automáticamente desde el formulario Pacientes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'PatientDocType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indica si el paciente clasifica como población especial: 1=Sí, 0=No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'SpecialPopulation'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código tipo de población especial, habilitado solo cuando SpecialPopulation=1. Valores: 01,02,10,14,16,17,22,25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'SpecialPopulationType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primer nombre del paciente, postulado desde el formulario Pacientes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'PatientFirstName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Segundo nombre del paciente, postulado desde el formulario Pacientes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'PatientSecondName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primer apellido del paciente, postulado desde el formulario Pacientes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'PatientFirstLastName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Segundo apellido del paciente, postulado desde el formulario Pacientes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'PatientSecondLastName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dirección de residencia principal del paciente, postulada desde el formulario Pacientes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'PatientAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del municipio de residencia del paciente, postulado desde la dirección principal en el formulario Pacientes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'PatientCityCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Teléfono del paciente: prioridad número móvil, luego teléfono fijo con indicativo. Editable si no tiene teléfono registrado, máximo 10 caracteres numéricos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'PatientPhone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Naturaleza del evento: 01=Accidente tránsito, 02=Sismo, 03=Maremoto, 04=Erupción volcánica, 05=Deslizamiento, 06=Inundación, 07=Avalancha, 08=Incendio natural, 09=Explosión terrorista, 10=Incendio terrorista, 11=Combate, 12=Ataques municipios, 13=Masacre, 14=Desplazados, 15=Mina antipersonal, 16=Huracán, 17=Otro, 25=Rayo, 26=Vendaval, 27=Tornado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'EventNature'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripción libre del evento cuando EventNature=17 (Otro). Máximo 40 caracteres, obligatorio cuando está habilitado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'OtherEventDescription'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Condición de la víctima, aplica solo cuando EventNature=01. Valores: 01=Conductor, 02=Peatón, 03=Ocupante, 04=Ciclista' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'VictimCondition'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de ocurrencia del evento AAAA-MM-DD. En accidente de tránsito se postula desde Fecha accidente del formulario Ingresos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'EventDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Zona de ocurrencia: 01=Rural, 02=Urbano. En accidente de tránsito se postula desde el formulario Ingresos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'EventZone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del departamento donde ocurrió el evento. En accidente de tránsito se postula desde el formulario Ingresos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'EventDepartment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del municipio donde ocurrió el evento. En accidente de tránsito se postula desde el formulario Ingresos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'EventCity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dirección del lugar de ocurrencia, máximo 100 caracteres. En accidente de tránsito se postula desde el formulario Ingresos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'EventAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripción corta del evento, mínimo 100 y máximo 1000 caracteres. En accidente de tránsito se postula desde el formulario Ingresos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'EventShortDescription'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de aseguramiento: 2=No asegurado, 3=Vehículo fantasma, 4=Póliza falsa, 6=Cobertura tarifa diferencial Decreto 2497/2022, 7=No asegurado propietario indeterminado, 8=Vehículo sin placa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'InsuranceStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Placa del vehículo, máximo 10 caracteres. Habilitado cuando InsuranceStatus es 2, 4, 6 o 7' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'VehiclePlateNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de vehículo: 01=Automóvil, 02=Bus, 03=Buseta, 04=Camión, 05=Camioneta, 06=Campero, 07=Microbús, 08=Tractocamión, 09=Transporte escolar, 10=Motocicleta, 14=Motocarro, 17=Mototriciclo, 19=Cuatrimoto, 20=Moto extranjera, 21=Vehículo extranjero, 22=Volqueta, 23=Transporte masivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'VehicleType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código aseguradora, máximo 6 caracteres. Habilitado cuando InsuranceStatus es 4 o 6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'InsurerCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de póliza SOAT, máximo 20 caracteres. Habilitado cuando InsuranceStatus es 4 o 6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'SoatPolicyNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha inicio vigencia póliza AAAA-MM-DD. No permite fechas mayores a la actual' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'PolicyStartDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha fin vigencia póliza AAAA-MM-DD. No permite fechas menores a PolicyStartDate' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'PolicyEndDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de radicado SIRAS, máximo 20 caracteres. Obligatorio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'SirasNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cobro agotamiento tope aseguradora: 1=Sí, 0=No. Habilitado cuando InsuranceStatus=6, precargado con 0 en otros casos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'InsurerCeilingCharge'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de identificación del propietario, parametrizado en maestro Parámetros formularios de reclamaciones' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'OwnerDocType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de documento del propietario, máximo 20 caracteres' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'OwnerDocNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primer nombre del propietario, máximo 30 caracteres' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'OwnerFirstName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Segundo nombre del propietario, máximo 30 caracteres' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'OwnerSecondName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primer apellido del propietario, máximo 30 caracteres' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'OwnerFirstLastName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Segundo apellido del propietario, máximo 30 caracteres' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'OwnerSecondLastName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dirección del propietario, máximo 100 caracteres. Inhabilitado cuando OwnerDocType=NI (NIT)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'OwnerAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Teléfono del propietario, máximo 10 dígitos. Inhabilitado cuando OwnerDocType=NI (NIT)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'OwnerPhone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del departamento del propietario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'OwnerDepartment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del municipio del propietario, dependiente del departamento seleccionado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'OwnerCity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de identificación del conductor, parametrizado en maestro Parámetros formularios de reclamaciones' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'DriverDocType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de documento del conductor, máximo 20 caracteres' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'DriverDocNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primer nombre del conductor, máximo 30 caracteres' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'DriverFirstName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Segundo nombre del conductor, máximo 30 caracteres' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'DriverSecondName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primer apellido del conductor, máximo 30 caracteres' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'DriverFirstLastName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Segundo apellido del conductor, máximo 30 caracteres' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'DriverSecondLastName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dirección del conductor, máximo 100 caracteres alfanuméricos y especiales (#, -, ., (), /)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'DriverAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Teléfono del conductor, máximo 10 dígitos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'DriverPhone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del departamento del conductor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'DriverDepartment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del municipio del conductor, dependiente del departamento seleccionado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'DriverCity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Uso de material de osteosíntesis: 1=Sí, 0=No. Obligatorio en accidente de tránsito' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'OsteosynthesisMaterial'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de atención: 1=Atención médica inicial, 2=Transporte primario, 3=Transporte secundario, 4=Continuidad hospitalaria, 5=Atención ambulatoria, 6=Atención médica+transporte primario, 7=Atención médica+transporte secundario, 8=Atención médica+transporte primario+secundario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'AttendanceType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Placa ambulancia traslado secundario, máximo 7 caracteres' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'SecondaryTransportPlate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo servicio transporte secundario: 1=Transporte básico, 2=Transporte medicalizado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'SecondaryTransportType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de habilitación del prestador que remite, máximo 12 caracteres numéricos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'ReferringProviderCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de habilitación del prestador que recibe. Se postula por defecto el código del cliente, máximo 12 caracteres numéricos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'ReceivingProviderCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de documento del profesional que recibe, parametrizado en maestro Parámetros formularios de reclamaciones' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'ReceivingProfessionalDocType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de documento del profesional que recibe, máximo 20 caracteres' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'ReceivingProfessionalDocNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de aceptación AAAA-MM-DD. No permite fechas posteriores a la actual' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'AcceptanceDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hora de aceptación HH:MM en formato 24 horas. No permite horas posteriores a la actual' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'AcceptanceTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo servicio transporte primario: 1=Transporte básico, 2=Transporte medicalizado. Habilitado cuando AttendanceType es 2, 6 u 8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'PrimaryTransportType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Placa ambulancia transporte primario, máximo 7 caracteres. Habilitado cuando AttendanceType es 2, 6 u 8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'PrimaryTransportPlate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de habilitación del prestador receptor. Se postula por defecto el código del cliente, máximo 12 caracteres numéricos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'ReceptorProviderCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dirección del sitio del evento, máximo 100 caracteres. Habilitado cuando AttendanceType es 2, 6 u 8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'EventSiteAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dirección de la IPS destino, máximo 100 caracteres. Habilitado cuando AttendanceType es 2, 6 u 8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'DestinationIpsAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que creó el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'CreatedBy'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora de creación del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'CreatedDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realizó la última modificación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora de la última modificación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'UpdatedDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de confirmación : 0. Sin Confirmar, 1. Confirmado, 2. Desconfirmado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'ConfirmationStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Profesional que confirma / desconfirma el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'ConfirmationUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que se confirma / desconfirma el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADFURIPS', @level2type=N'COLUMN',@level2name=N'ConfirmationDate'
GO