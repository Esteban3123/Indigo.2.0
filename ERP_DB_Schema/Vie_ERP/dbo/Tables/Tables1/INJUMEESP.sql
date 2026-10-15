CREATE TABLE [dbo].[INJUMEESP] (
    [CODJUMEES] CHAR (3)      NOT NULL,
    [DESJUMEES] VARCHAR (100) NOT NULL,
    [ESTJUMEES] BIT           NOT NULL,
    CONSTRAINT [PK_INJUMEESP] PRIMARY KEY CLUSTERED ([CODJUMEES] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la justificacion de medicamentos especiales ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INJUMEESP', @level2type = N'COLUMN', @level2name = N'ESTJUMEES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la justificacion de medicamentos especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INJUMEESP', @level2type = N'COLUMN', @level2name = N'DESJUMEES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la justificacion de medicamentos especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INJUMEESP', @level2type = N'COLUMN', @level2name = N'CODJUMEES';

