CREATE TABLE [Nursing].[IndicationsTherapeuticRestraints] (
    [idTherapeuticRestraints] INT      NOT NULL,
    [CodeIndication]          CHAR (4) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la indicación de las restricciones terapeuticas', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'IndicationsTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'CodeIndication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id de la tabla TherapeuticRestraints', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'IndicationsTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'idTherapeuticRestraints';

