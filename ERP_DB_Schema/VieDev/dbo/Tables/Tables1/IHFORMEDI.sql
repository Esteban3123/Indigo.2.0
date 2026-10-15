CREATE TABLE [dbo].[IHFORMEDI] (
    [CODFORMED]            VARCHAR (20) NOT NULL,
    [DESFORMED]            CHAR (100)   NOT NULL,
    [CODHOMHV]             VARCHAR (20) NULL,
    [RequireStability]     BIT          NULL,
    [AllowExtramural]      BIT          CONSTRAINT [DF_IHFORMEDI_AllowExtramural] DEFAULT ((0)) NOT NULL,
    [AutomaticCalculation] BIT          CONSTRAINT [DF_IHFORMEDI_AutomaticCalculation] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_IHFORMEDI] PRIMARY KEY CLUSTERED ([CODFORMED] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '05-07-2023 me indica si calcula de forma automatica las cantidades en la prescripcion medica, por ahora solo para medicamentos extramurales que son medicamentos adicionales.   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'AutomaticCalculation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '05-07-2023 me indica si la forma medica se lista en la creaciòn de un producto adicional en la  una hc extramural por parte del medico. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI', @level2type = N'COLUMN', @level2name = N'AllowExtramural';

