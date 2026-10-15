CREATE TABLE [dbo].[HCVACADIPAC] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDVACUNA]      INT                                                                              NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [FECHAPLICAVAC] DATETIME                                                                         NULL,
    [DOSIS]         VARCHAR (60)                                                                     NULL,
    [OBSVACUNA]     CHAR (200)                                                                       NULL,
    [MOTNOAPLICA]   TINYINT                                                                          NULL,
    [CODCENATE]     CHAR (10)                                                                        NULL,
    [CODENTIDA]     CHAR (9)                                                                         NULL,
    [GENCONENTITY]  INT                                                                              NULL,
    [LOTEDOSIS]     VARCHAR (20)                                                                     NULL,
    [LOTEJERINGA]   VARCHAR (20)                                                                     NULL,
    [ADMOTRINS]     BIT                                                                              NULL,
    [APLICADA]      BIT                                                                              NULL,
    [NUMINGRES]     CHAR (10)                                                                        NULL,
    [USUREGISTRA]   VARCHAR (200)                                                                    NULL,
    CONSTRAINT [PK_HCVACADIPAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCVACADIPAC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCVACADIPAC_HCVACUADI] FOREIGN KEY ([IDVACUNA]) REFERENCES [dbo].[HCVACUADI] ([ID]),
    CONSTRAINT [FK_HCVACADIPAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCVACADIPAC] NOCHECK CONSTRAINT [FK_HCVACADIPAC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCVACADIPAC] NOCHECK CONSTRAINT [FK_HCVACADIPAC_HCVACUADI];


GO
ALTER TABLE [dbo].[HCVACADIPAC] NOCHECK CONSTRAINT [FK_HCVACADIPAC_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario resgistrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'USUREGISTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ingreso que se guarda solo cuando el registro se hace  dede una Historia clinica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que indica si la vacuna fue aplicada 1=Si 0=NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'APLICADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que indica si la vacuna fue administrada por otra institución 1=Si 0=NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'ADMOTRINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lote de la jeringa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'LOTEJERINGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lote de Dosis de la vacuna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'LOTEDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Empresa administradora nativa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo no Aplicación  0- Aplicada  1- No se Administra por una Tradición,   2- No se Administra por una Condición de Salud,  3- No se Administra por Negación del Usuario,  4- No se Administra por tener datos del contacto del usuario no actualizado,   5- No se administra por otras razones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'MOTNOAPLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion de Vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'OBSVACUNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'DOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de aplicacion de la vacuna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'FECHAPLICAVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Vacuna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'IDVACUNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'ID';

