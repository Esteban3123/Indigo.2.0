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
ADD SENSITIVITY CLASSIFICATION TO
    [Admissions].[Furtran].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Admissions].[Furtran].[VictimPolicyNumber]
    WITH (LABEL = 'Confidential - Financial', INFORMATION_TYPE = 'Financial');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Admissions].[Furtran].[VictimSIRASFilingNumber]
    WITH (LABEL = 'Confidential - Financial', INFORMATION_TYPE = 'Financial');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor reclamado por la víctima en accidente de tránsito, glosa o evento catastrófico/terrorista. BIGINT, monto en pesos COP.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'ClaimedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el valor reclamado.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'ClaimedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'ClaimedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor facturado por la IPS prestadora de servicios de urgencia/atención. BIGINT, monto en pesos COP, base para glosa.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'InvoicedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el valor facturado.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'InvoicedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'InvoicedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicado SIRAS (Sistema de Información de Reportes de Accidentes de Tránsito), mascarado, para eventos de tránsito. VARCHAR(20), PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimSIRASFilingNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el número de radicado SIRAS si el evento es un accidente de  tránsito.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimSIRASFilingNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimSIRASFilingNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de vigencia de póliza de seguros de la víctima en accidente de tránsito. DATE, validación de cobertura.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPolicyEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la fecha final vigencia poliza si el evento es un accidente de  tránsito.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPolicyEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPolicyEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio vigencia póliza de seguros de la víctima en accidente de tránsito. DATE, validación de cobertura.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPolicyStartDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la fecha de inicio vigencia poliza si el evento es un accidente de  tránsito.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPolicyStartDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPolicyStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de póliza de seguros de la víctima, mascarado PII, accidente de tránsito. VARCHAR(20), PolicyNumber_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPolicyNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el número de la poliza si el evento es un accidente de  tránsito.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPolicyNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPolicyNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de la aseguradora/compañía de seguros de la víctima. CHAR(9), accidente de tránsito.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimInsuranceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código aseguradora si el evento es un accidente de  tránsito.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimInsuranceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimInsuranceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Placa del vehículo involucrado (víctima) en accidente de tránsito. VARCHAR(10), para identificación y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimVehiclePlate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la placa del vehículo si el evento es un accidente de  tránsito.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimVehiclePlate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimVehiclePlate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vehículo de la víctima: 1-Automóvil, 2-Bus, 3-Buseta, 4-Camión, 5-Camioneta, 6-Campero, 7-Microbús, 8-Tractocamión, 9-Motocicleta, 10-Motocarro, 11-Moto triciclo, 12-Cuatrimoto, 13-Moto extranjera, 14-Vehículo extranjero, 15-Volqueta. INT.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimVehicleType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el tipo de vehículo involucrado si el evento es un accidente de  tránsito.  1 - Automóvil  2 - Bus  3 - Buseta  4 - Camión  5 - Camioneta  6 - Campero  7 - Microbús  8 - Tractocamión  9 - Motocicleta  10 - Motocarro  11 - Moto triciclo  12 - Cuatrimoto  13 - Moto extranjera  14 - Vehículo extranjero  15 - Volqueta', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimVehicleType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimVehicleType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de aseguramiento de la víctima: 1-Asegurado, 2-No asegurado, 3-Vehículo fantasma, 4-Póliza falsa, 5-No asegurado (Propietario indeterminado), 6-No asegurado sin placa. INT.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimAssuranceStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el estado de aseguramiento si el evento es un accidente de  tránsito.  1 - Asegurado  2 - No asegurado  3 - Vehículo fantasma  4 - Póliza falsa  5 - No asegurado - Propietario indeterminado o sin información  6 - No asegurado- Sin placa', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimAssuranceStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimAssuranceStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición/rol de la víctima en accidente: 1-Conductor, 2-Peatón, 3-Ocupante, 4-Ciclista. INT, para clasificación de responsabilidad.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCondition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la condición de la víctima si el evento es un accidente de  tránsito.  1 - Conductor  2 - Peatón  3 - Ocupante  4 - Ciclista', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCondition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCondition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT si el evento constituye manifestación de servicios habilitados por la aseguradora. True/False.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'ManifestationOfEnabledServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena si es manifestación de servicios habilitados.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'ManifestationOfEnabledServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'ManifestationOfEnabledServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto en la IPS receptora (traslado/derivación) de la víctima. VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificatePhoneNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el número de teléfono de  traslado.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificatePhoneNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificatePhoneNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de la IPS receptora donde se trasladó la víctima. VARCHAR(20), ubicación de atención.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateAddress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la dirección de traslado.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateAddress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del municipio de la IPS receptora (traslado de víctima). CHAR(5), FK INMUNICIP.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateMunicipalityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código del municipio a donde se traslada la víctima.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateMunicipalityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateMunicipalityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del departamento de la IPS receptora (traslado de víctima). CHAR(2), FK INDEPARTA.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateDepartmentCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código del departamento a donde se traslada la víctima.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateDepartmentCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateDepartmentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la IPS receptora para traslado/atención de la víctima. CHAR(100), FK ADCONTIPS.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código de la IPS receptora.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de traslado/derivación de la víctima a IPS receptora. TIME.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateTransferTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la hora de traslado de la víctima', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateTransferTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateTransferTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de traslado/derivación de la víctima a IPS receptora. DATE.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateTransferDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la fecha de traslado de la víctima.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateTransferDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimCertificateTransferDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Zona geográfica donde se recogió la víctima: True-Urbana, False-Rural. BIT.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceZone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la zona donde se recoge la víctima.  True - Urbana  False - Rural', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceZone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceZone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código municipio del lugar de pickup/rescate de la víctima. CHAR(5), FK INMUNICIP.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceMunicipalityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código del municipio donde se recoge la víctima.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceMunicipalityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceMunicipalityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código departamento del lugar de pickup/rescate de la víctima. CHAR(2), FK INDEPARTA.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceDepartmentCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código del departamento donde se recoge la víctima.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceDepartmentCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceDepartmentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección/ubicación donde se recogió la víctima en accidente o evento catastrófico. VARCHAR(40).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceAddress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la dirección del lugar donde se recoge la víctima.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceAddress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimPlaceAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de evento: 1-Accidente de tránsito, 2-Evento catastrófico, 3-Evento terrorista. INT, clasificación para RIPS.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'EvenType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Tipo de evento.  1 - Accidente de tránsito  2 - Evento catastrófico  3 - Evento terrorista', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'EvenType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'EvenType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo de la víctima: 1-Masculino, 2-Femenino, 3-Otro. INT.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimSex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el sexo de la víctima.  1 - Masculino  2 - Femenino  3 - Otro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimSex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'VictimSex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Placa del vehículo del conductor/unidad de atención (ambulancia u otro). VARCHAR(10).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverVehiclePlate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la placa del vehículo del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverVehiclePlate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverVehiclePlate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vehículo del conductor: 1-Ambulancia básica, 2-Ambulancia medicalizada, 3-Particular, 4-Público, 5-Oficial, 6-Emergencia, 7-Diplomático, 8-Transporte masivo, 9-Escolar. INT.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverVehicleType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el tipo de vehículo del conductor.  1 - Ambulancia básica  2 - Ambulancia medicalizada  3 - Particular  4 - Público  5 - Oficial  6 - De emergencia  7 - Diplomático o consular  8 - Transporte masivo  9 - Escolar', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverVehicleType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverVehicleType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código municipio de residencia/ubicación del conductor. CHAR(5), FK INMUNICIP.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverMunicipalityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código del municipio del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverMunicipalityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverMunicipalityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código departamento de residencia/ubicación del conductor. CHAR(2), FK INDEPARTA.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverDepartmentCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código del departamento del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverDepartmentCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverDepartmentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto del conductor/profesional de atención. VARCHAR(10).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverPhoneNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el número de teléfono del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverPhoneNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverPhoneNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de residencia del conductor/profesional sanitario. VARCHAR(40).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverAddress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la dirección del conductor', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverAddress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cédula/identificación del conductor (PII). VARCHAR(25), clave para trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverIdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el número de identificación del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverIdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverIdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación del conductor: CC, pasaporte, etc. INT, relacionado a documento legal.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverIdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el tipo de identificación del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverIdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverIdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del conductor/profesional de atención. VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverSecondLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el segundo apellido del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverSecondLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverSecondLastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del conductor/profesional de atención. VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverFirstLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el primer apellido del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverFirstLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverFirstLastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del conductor/profesional de atención. VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverSecondName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el segundo nombre del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverSecondName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverSecondName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del conductor/profesional de atención. VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverFirstName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el primer nombre del conductor.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverFirstName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'DriverFirstName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de habilitación de la IPS prestadora de servicios de urgencia. CHAR(100), FK ADCONTIPS.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'IPSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código de la IPS del campo Código habilitación prestador.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'IPSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'IPSCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de radicación/presentación del evento en el sistema. DATE, crítica para RIPS y glosas.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'FilingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la feha de radicación.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'FilingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'FilingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta a glosa/objeción: 1-Glosa total, 2-Pago parcial, 3-Glosa transversal. INT.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'GlossyResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la respuesta a glosa u objeción.  1 - Glosa u objeción total  2 - Pago parcial  3 - Glosa transversal', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'GlossyResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'GlossyResponse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicado anterior (si existe reradicación/reajuste). VARCHAR(10).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'PreviousFilingNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el número de radicado anterior.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'PreviousFilingNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'PreviousFilingNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura emitida por la IPS. VARCHAR(20), documento contable principal.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el número de la factura.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión del paciente-víctima (FK ADINGRESO). CHAR(10).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'EntryNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el número de ingreso del paciente(Víctima).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'EntryNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'EntryNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del paciente-víctima, mascarado PII (cédula/documento). VARCHAR(25), Identification_Ofuscado, FK INPACIENT.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código del paciente (Víctima)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial de registro de accidente/evento (clave primaria). INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de radicaciones FURTRAN (Fondo de Atención de Urgencias por Accidentes de Tránsito) que consolida la información del conductor, la víctima, el evento vial, la póliza de seguro obligatorio (SOAT) y los valores facturados y reclamados ante la aseguradora o el ADRES por atenciones de urgencia derivadas de accidentes de tránsito.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Furtran';
