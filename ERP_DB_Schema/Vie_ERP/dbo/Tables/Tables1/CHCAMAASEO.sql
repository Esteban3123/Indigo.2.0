CREATE TABLE [dbo].[CHCAMAASEO] (
    [ID]             INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODICAMAS]      INT       NOT NULL,
    [ESTADO]         INT       NOT NULL,
    [FECHAINI]       DATETIME  NULL,
    [FECHAFIN]       DATETIME  NULL,
    [USUARIOINI]     CHAR (20) NULL,
    [USUARIOFIN]     CHAR (20) NULL,
    [USUARIOREG]     CHAR (20) NOT NULL,
    [FECHAREG]       DATETIME  NOT NULL,
    [USUARIOMOD]     CHAR (20) NULL,
    [FECHAMOD]       DATETIME  NULL,
    [ULTIMALIMPIEZA] DATETIME  NULL,
    [ULTIMAJORNADA]  DATETIME  NOT NULL,
    [LIMPIEZAEXTRA]  BIT       NOT NULL,
    [TIPOLIMPIEZA]   INT       NOT NULL,
    CONSTRAINT [PK_CHCAMAASEO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHCAMAASEO_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS])
);


GO
ALTER TABLE [dbo].[CHCAMAASEO] NOCHECK CONSTRAINT [FK_CHCAMAASEO_CHCAMASHO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1= Rutinaria  2= Extra  3= Terminal  Resto = Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'TIPOLIMPIEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que indica si hubo limpieza extra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'LIMPIEZAEXTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ultima fecha y hora que se inicio la jornada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ULTIMAJORNADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la ultima limpieza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ULTIMALIMPIEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creacion registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario crea registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que finalizo la limpieza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que inicio la limpieza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final limpieza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial inicio jornada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Inicia Jornada (color rojo)  2 - Inicia Limpieza (color amarillo)  3 - Finaliza Limpieza (color verde)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de relacion con cama CAMASHO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ID';

