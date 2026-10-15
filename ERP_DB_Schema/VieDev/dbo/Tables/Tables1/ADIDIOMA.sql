CREATE TABLE [dbo].[ADIDIOMA] (
    [IDICODIGO]           VARCHAR (3)   NOT NULL,
    [IDIDESCRI]           VARCHAR (100) NOT NULL,
    [ESTADO]              BIT           CONSTRAINT [DF_ADIDIOMA_ESTADO] DEFAULT ((0)) NOT NULL,
    [FECHACREACION]       DATETIME      NULL,
    [USUARIOCREACION]     CHAR (20)     NULL,
    [FECHAMODIFICACION]   DATETIME      NULL,
    [USUARIOMODIFICACION] CHAR (20)     NULL,
    CONSTRAINT [PK_ADIDIOMA] PRIMARY KEY CLUSTERED ([IDICODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 = TRUE = ACTIVO    0 = FALSE = INACTIVO   POR DEFECTO FALSE ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADIDIOMA', @level2type = N'COLUMN', @level2name = N'ESTADO';

