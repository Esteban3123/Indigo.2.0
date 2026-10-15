CREATE TABLE [dbo].[HCENTREGATURNOCUPS] (
    [ID]                      INT        IDENTITY (1, 1) NOT NULL,
    [IDHCENTREGATURNOC]       INT        NOT NULL,
    [IDHCENTREGATURNOCPACIEN] INT        NOT NULL,
    [TIPOSERVICIO]            INT        NOT NULL,
    [NUMEFOLIO]               NCHAR (10) NOT NULL,
    [CODSERIPS]               CHAR (20)  NOT NULL,
    [ESTADO]                  INT        NOT NULL,
    CONSTRAINT [PK_HCENTREGATURNOLAB_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCENTREGATURNOCUPS_HCENTREGATURNOC] FOREIGN KEY ([IDHCENTREGATURNOC]) REFERENCES [dbo].[HCENTREGATURNOC] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOCUPS_HCENTREGATURNOCPACIEN] FOREIGN KEY ([IDHCENTREGATURNOCPACIEN]) REFERENCES [dbo].[HCENTREGATURNOCPACIEN] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOCUPS_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[HCENTREGATURNOCUPS] NOCHECK CONSTRAINT [FK_HCENTREGATURNOCUPS_INCUPSIPS];


GO
CREATE NONCLUSTERED INDEX [IX_HCENTREGATURNOCUPS_IDHCENTREGATURNOC_IDHCENTREGATURNOCPACIEN]
    ON [dbo].[HCENTREGATURNOCUPS]([IDHCENTREGATURNOC] ASC, [IDHCENTREGATURNOCPACIEN] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estados CUPS    --Imagenes--  Estado Servicio IPS  1: Solicitado  2: Estudio Realizado  3: Imagen Procesada  4: Estudio Interpretado  5: Remitido  6: Anulado  7: Extramural    -- Interconsultas --  1: Solicitado  2: Solicitud Enviada  3: Interconsulta Realizada  4: Extramural  5: Anulado    -- Laboratorios --  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural  8:Muestra Recolectada Parcialmente    --Patologias--  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural    --ProcedimientosNoQx--  1: Ordenado  2: Completado  3: Interpretado  4: Sin Interfaz  5: Anulado  6: Sala programada    -- ProcedimientosQx --  1: Solicitado  2: Sala Programada  3: Cancelado  4: Resultado Revisado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Procedimiento (CUPS)  1 : Imagenes_Dx  2 : Interconsultas   3 : Laboratorios  4 : Patologias   5 : ProcedimientosNoQx   6 : ProcedimientosQx   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'TIPOSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla cabecera HCENTREGATURNOCPACIENTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOCPACIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla cabecera HCENTREGATURNO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'ID';

