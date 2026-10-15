CREATE TABLE [dbo].[INAUTORIG] (
    [PKTIPOCON] INT          NOT NULL,
    [CODGRUPOU] CHAR (3)     NOT NULL,
    [PKVALORCO] CHAR (20)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_INAUTORIG] PRIMARY KEY CLUSTERED ([PKTIPOCON] ASC, [CODGRUPOU] ASC, [PKVALORCO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1: Centros de Atencion  2: Unidades Funcionales  3: Platillas de Edicion  4: Abrir mas de un Ingreso  5: Entidades  6: Agendamiento: Especialidades  7: Agendamiento: Salas  8: Dashboard: Sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIG', @level2type = N'COLUMN', @level2name = N'PKTIPOCON';

