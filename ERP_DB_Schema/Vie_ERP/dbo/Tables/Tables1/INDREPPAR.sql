CREATE TABLE [dbo].[INDREPPAR] (
    [CODREPORT] NCHAR (3)   NOT NULL,
    [NOMREPORT] NCHAR (100) NOT NULL,
    [MODREPORT] NCHAR (13)  NOT NULL,
    [ID]        INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_INDREPPAR] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación consecutiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modulo:     1= ''Historias Clinicas''   2= ''Ley 100''   3= ''Ordenes Medicas IntraHospitalarias''  4= ''Ordenes Medicas Extramurales''   5=''Otros''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'MODREPORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'NOMREPORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'CODREPORT';

