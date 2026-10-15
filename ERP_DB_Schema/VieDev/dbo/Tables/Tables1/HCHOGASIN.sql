CREATE TABLE [dbo].[HCHOGASIN] (
    [CONSECUTI]                NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FECHAUTIL]                DATETIME                                                                         NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [CODPRODUC]                CHAR (20)                                                                        NULL,
    [CANUTIPRO]                INT                                                                              NULL,
    [TIPORIGEN]                CHAR (1)                                                                         NOT NULL,
    [OBSERVACI]                VARCHAR (MAX)                                                                    NULL,
    [CODACTENF]                CHAR (3)                                                                         NULL,
    [CANACTENF]                INT                                                                              NULL,
    [VALACTENF]                DECIMAL (18, 2)                                                                  NULL,
    [GENSERVICEORDER]          INT                                                                              NULL,
    [IdNursingPackage]         INT                                                                              NULL,
    [CODSERIPS]                CHAR (20)                                                                        NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    CONSTRAINT [PK_HCHOGASIN] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCHOGASIN_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCHOGASIN_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCHOGASIN_AGPAQUETES] FOREIGN KEY ([IdNursingPackage]) REFERENCES [dbo].[AGPAQUETES] ([ID]),
    CONSTRAINT [FK_HCHOGASIN_HCACTENFE] FOREIGN KEY ([CODACTENF]) REFERENCES [dbo].[HCACTENFE] ([CODACTENF]),
    CONSTRAINT [FK_HCHOGASIN_HCHOGASIN] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCHOGASIN_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCHOGASIN_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCHOGASIN] NOCHECK CONSTRAINT [FK_HCHOGASIN_ADCENATEN];


GO
ALTER TABLE [dbo].[HCHOGASIN] NOCHECK CONSTRAINT [FK_HCHOGASIN_ADINGRESO];


GO
ALTER TABLE [dbo].[HCHOGASIN] NOCHECK CONSTRAINT [FK_HCHOGASIN_AGPAQUETES];


GO
ALTER TABLE [dbo].[HCHOGASIN] NOCHECK CONSTRAINT [FK_HCHOGASIN_HCACTENFE];


GO
ALTER TABLE [dbo].[HCHOGASIN] NOCHECK CONSTRAINT [FK_HCHOGASIN_HCHOGASIN];


GO
ALTER TABLE [dbo].[HCHOGASIN] NOCHECK CONSTRAINT [FK_HCHOGASIN_INPACIENT];


GO
ALTER TABLE [dbo].[HCHOGASIN] NOCHECK CONSTRAINT [FK_HCHOGASIN_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCHOGASIN_NUMINGRES_CODCENATE_CODACTENF_CODPROSAL_CONSECUTI_FECHAUTIL_GENSERVICEORDER_IPCODPACI_OBSERVACI_UFUCODIGO]
    ON [dbo].[HCHOGASIN]([NUMINGRES] ASC, [CODCENATE] ASC)
    INCLUDE([CODACTENF], [CODPROSAL], [CONSECUTI], [FECHAUTIL], [GENSERVICEORDER], [IPCODPACI], [OBSERVACI], [UFUCODIGO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la descripción relacionada del servicio seleccionado, de la tabla: Contract.CUPSEntityContractDescriptions
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio CUPS de la actividad de enfermería', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el id del paquete si el insumo fue agregado desde la rejilla de paquetes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'IdNursingPackage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Origen:  1. Medicamentos  2. Mezclas y Liquidos  3. Enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'TIPORIGEN';

