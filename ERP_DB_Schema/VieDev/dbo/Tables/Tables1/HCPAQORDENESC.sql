CREATE TABLE [dbo].[HCPAQORDENESC] (
    [ID]              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]          VARCHAR (3)  NOT NULL,
    [NOMBRE]          VARCHAR (80) NOT NULL,
    [ESTADO]          BIT          NOT NULL,
    [USUARIOCREACION] CHAR (20)    NOT NULL,
    [FECHACREACION]   DATETIME     NOT NULL,
    [USUARIOMODIFICA] CHAR (20)    NULL,
    [FECHAMODIFA]     DATETIME     NULL,
    CONSTRAINT [PK_HCPAQORDENESC] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESC', @level2type = N'COLUMN', @level2name = N'ESTADO';

