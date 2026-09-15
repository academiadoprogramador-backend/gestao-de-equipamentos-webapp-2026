CREATE TABLE dbo.TBEquipamentos
(
    Id INT IDENTITY(1, 1) NOT NULL,
    Nome NVARCHAR(100) NOT NULL,
    PrecoAquisicao DECIMAL(18, 2) NOT NULL,
    DataFabricacao DATE NOT NULL,
    FabricanteId INT NOT NULL,

    CONSTRAINT PK_TBEquipamentos PRIMARY KEY (Id),
    CONSTRAINT CK_TBEquipamentos_Nome CHECK (LEN(LTRIM(RTRIM(Nome))) BETWEEN 6 AND 100),
    CONSTRAINT CK_TBEquipamentos_PrecoAquisicao CHECK (PrecoAquisicao > 0),
    CONSTRAINT FK_TBEquipamentos_TBFabricantes FOREIGN KEY (FabricanteId)
        REFERENCES dbo.TBFabricantes (Id)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);

CREATE INDEX IX_TBEquipamentos_FabricanteId
    ON dbo.TBEquipamentos (FabricanteId);
