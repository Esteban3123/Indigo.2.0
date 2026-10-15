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
CREATE NONCLUSTERED INDEX [IX_HCHOGASIN]
    ON [dbo].[HCHOGASIN]([IPCODPACI] ASC, [NUMINGRES] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la descripción relacionada del servicio seleccionado, de la tabla: Contract.CUPSEntityContractDescriptions
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio CUPS de la actividad de enfermería', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el id del paquete si el insumo fue agregado desde la rejilla de paquetes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'IdNursingPackage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la orden de servicio en la cual quedo incluido el insumo de enfermeria, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la actividad de enfermería', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'VALACTENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad actividad de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'CANACTENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Actividad de Enfermeria ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'CODACTENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones Adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Origen:  1. Medicamentos  2. Mezclas y Liquidos  3. Enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'TIPORIGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Consumida del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'CANUTIPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto Gastado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'FECHAUTIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOGASIN', @level2type = N'COLUMN', @level2name = N'CONSECUTI';

