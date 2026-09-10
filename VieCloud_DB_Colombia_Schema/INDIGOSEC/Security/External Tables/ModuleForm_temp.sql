CREATE EXTERNAL TABLE [Security].[ModuleForm_temp] (
    [Id] INT NOT NULL,
    [IdModule] INT NOT NULL,
    [IdTitle] INT NOT NULL,
    [IdForm] INT NOT NULL,
    [FormOrder] INT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC_TEMP]
    );

