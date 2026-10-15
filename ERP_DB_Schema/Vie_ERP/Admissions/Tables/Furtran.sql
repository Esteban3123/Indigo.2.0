CREATE TABLE [Admissions].[Furtran] (
    [Id]                                INT                                                                              IDENTITY (1, 1) NOT NULL,
    [PatientCode]                       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [EntryNumber]                       CHAR (10)                                                                        NOT NULL,
    [InvoiceNumber]                     VARCHAR (20)                                                                     NULL,
    [PreviousFilingNumber]              VARCHAR (10)                                                                     NULL,
    [GlossyResponse]                    INT                                                                              NULL,
    [FilingDate]                        DATE                                                                             NOT NULL,
    [IPSCode]                           CHAR (100)                                                                       NULL,
    [DriverFirstName]                   VARCHAR (20)                                                                     NOT NULL,
    [DriverSecondName]                  VARCHAR (20)                                                                     NULL,
    [DriverFirstLastName]               VARCHAR (20)                                                                     NOT NULL,
    [DriverSecondLastName]              VARCHAR (20)                                                                     NULL,
    [DriverIdentificationType]          INT                                                                              NULL,
    [DriverIdentificationNumber]        VARCHAR (25)                                                                     NOT NULL,
    [DriverAddress]                     VARCHAR (40)                                                                     NOT NULL,
    [DriverPhoneNumber]                 VARCHAR (10)                                                                     NOT NULL,
    [DriverDepartmentCode]              CHAR (2)                                                                         NOT NULL,
    [DriverMunicipalityCode]            CHAR (5)                                                                         NOT NULL,
    [DriverVehicleType]                 INT                                                                              NOT NULL,
    [DriverVehiclePlate]                VARCHAR (10)                                                                     NOT NULL,
    [VictimSex]                         INT                                                                              NULL,
    [EvenType]                          INT                                                                              NULL,
    [VictimPlaceAddress]                VARCHAR (40)                                                                     NOT NULL,
    [VictimPlaceDepartmentCode]         CHAR (2)                                                                         NOT NULL,
    [VictimPlaceMunicipalityCode]       CHAR (5)                                                                         NOT NULL,
    [VictimPlaceZone]                   BIT                                                                              NOT NULL,
    [VictimCertificateTransferDate]     DATE                                                                             NOT NULL,
    [VictimCertificateTransferTime]     TIME (7)                                                                         NOT NULL,
    [VictimCertificateIPS]              CHAR (100)                                                                       NOT NULL,
    [VictimCertificateDepartmentCode]   CHAR (2)                                                                         NOT NULL,
    [VictimCertificateMunicipalityCode] CHAR (5)                                                                         NOT NULL,
    [VictimCertificateAddress]          VARCHAR (20)                                                                     NULL,
    [VictimCertificatePhoneNumber]      VARCHAR (20)                                                                     NULL,
    [ManifestationOfEnabledServices]    BIT                                                                              NULL,
    [VictimCondition]                   INT                                                                              NULL,
    [VictimAssuranceStatus]             INT                                                                              NULL,
    [VictimVehicleType]                 INT                                                                              NULL,
    [VictimVehiclePlate]                VARCHAR (10)                                                                     NULL,
    [VictimInsuranceCode]               CHAR (9)                                                                         NULL,
    [VictimPolicyNumber]                VARCHAR (20) MASKED WITH (FUNCTION = 'partial(0, "PolicyNumber_Ofuscado", 0)')   NULL,
    [VictimPolicyStartDate]             DATE                                                                             NULL,
    [VictimPolicyEndDate]               DATE                                                                             NULL,
    [VictimSIRASFilingNumber]           VARCHAR (20) MASKED WITH (FUNCTION = 'partial(0, "NumberSIRAS_Ofuscado", 0)')    NULL,
    [InvoicedValue]                     BIGINT                                                                           NOT NULL,
    [ClaimedValue]                      BIGINT                                                                           NOT NULL,
    CONSTRAINT [PK_Furtran] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Furtran_ADCONTIPS] FOREIGN KEY ([IPSCode]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS]),
    CONSTRAINT [FK_Furtran_ADCONTIPS_Certificate] FOREIGN KEY ([VictimCertificateIPS]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS]),
    CONSTRAINT [FK_Furtran_ADINGRESO] FOREIGN KEY ([EntryNumber]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_Furtran_Driver_INDEPARTA] FOREIGN KEY ([DriverDepartmentCode]) REFERENCES [dbo].[INDEPARTA] ([depcodigo]),
    CONSTRAINT [FK_Furtran_Driver_INMUNICIP] FOREIGN KEY ([DriverMunicipalityCode]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD]),
    CONSTRAINT [FK_Furtran_INPACIENT] FOREIGN KEY ([PatientCode]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_Furtran_VictimCertificate_INDEPARTA] FOREIGN KEY ([VictimCertificateDepartmentCode]) REFERENCES [dbo].[INDEPARTA] ([depcodigo]),
    CONSTRAINT [FK_Furtran_VictimCertificate_INMUNICIP] FOREIGN KEY ([VictimCertificateMunicipalityCode]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD]),
    CONSTRAINT [FK_Furtran_VictimPlace_INDEPARTA] FOREIGN KEY ([VictimPlaceDepartmentCode]) REFERENCES [dbo].[INDEPARTA] ([depcodigo]),
    CONSTRAINT [FK_Furtran_VictimPlace_INMUNICIP] FOREIGN KEY ([VictimPlaceMunicipalityCode]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el valor reclamado.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'ClaimedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el valor facturado.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'InvoicedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el número de radicado SIRAS si el evento es un accidente de  tránsito.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimSIRASFilingNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la fecha final vigencia poliza si el evento es un accidente de  tránsito.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPolicyEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la fecha de inicio vigencia poliza si el evento es un accidente de  tránsito.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPolicyStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el número de la poliza si el evento es un accidente de  tránsito.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPolicyNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el código aseguradora si el evento es un accidente de  tránsito.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimInsuranceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la placa del vehículo si el evento es un accidente de  tránsito.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimVehiclePlate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el tipo de vehículo involucrado si el evento es un accidente de  tránsito.  1 - Automóvil  2 - Bus  3 - Buseta  4 - Camión  5 - Camioneta  6 - Campero  7 - Microbús  8 - Tractocamión  9 - Motocicleta  10 - Motocarro  11 - Moto triciclo  12 - Cuatrimoto  13 - Moto extranjera  14 - Vehículo extranjero  15 - Volqueta', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimVehicleType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el estado de aseguramiento si el evento es un accidente de  tránsito.  1 - Asegurado  2 - No asegurado  3 - Vehículo fantasma  4 - Póliza falsa  5 - No asegurado - Propietario indeterminado o sin información  6 - No asegurado- Sin placa', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimAssuranceStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la condición de la víctima si el evento es un accidente de  tránsito.  1 - Conductor  2 - Peatón  3 - Ocupante  4 - Ciclista', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCondition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena si es manifestación de servicios habilitados.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'ManifestationOfEnabledServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el número de teléfono de  traslado.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificatePhoneNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la dirección de traslado.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el código del municipio a donde se traslada la víctima.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateMunicipalityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el código del departamento a donde se traslada la víctima.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateDepartmentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el código de la IPS receptora.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la hora de traslado de la víctima', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateTransferTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la fecha de traslado de la víctima.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateTransferDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la zona donde se recoge la víctima.  True - Urbana  False - Rural', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceZone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el código del municipio donde se recoge la víctima.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceMunicipalityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el código del departamento donde se recoge la víctima.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceDepartmentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la dirección del lugar donde se recoge la víctima.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Tipo de evento.  1 - Accidente de tránsito  2 - Evento catastrófico  3 - Evento terrorista', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'EvenType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el sexo de la víctima.  1 - Masculino  2 - Femenino  3 - Otro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimSex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la placa del vehículo del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverVehiclePlate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el tipo de vehículo del conductor.  1 - Ambulancia básica  2 - Ambulancia medicalizada  3 - Particular  4 - Público  5 - Oficial  6 - De emergencia  7 - Diplomático o consular  8 - Transporte masivo  9 - Escolar', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverVehicleType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el código del municipio del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverMunicipalityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el código del departamento del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverDepartmentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el número de teléfono del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverPhoneNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la dirección del conductor', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el número de identificación del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverIdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el tipo de identificación del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverIdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el segundo apellido del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverSecondLastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el primer apellido del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverFirstLastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el segundo nombre del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverSecondName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el primer nombre del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverFirstName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el código de la IPS del campo Código habilitación prestador.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'IPSCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la feha de radicación.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'FilingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la respuesta a glosa u objeción.  1 - Glosa u objeción total  2 - Pago parcial  3 - Glosa transversal', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'GlossyResponse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el número de radicado anterior.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'PreviousFilingNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el número de la factura.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el número de ingreso del paciente(Víctima).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'EntryNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el código del paciente (Víctima)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'Id';

