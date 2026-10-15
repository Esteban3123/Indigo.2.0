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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Inicia Jornada (color rojo)  2 - Inicia Limpieza (color amarillo)  3 - Finaliza Limpieza (color verde)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ESTADO';

