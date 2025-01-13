# **IEL**  
**Descrição:**  
Este projeto foi feito para testar minhas capacidades na segunda etapa do estágio da IEL, criar um CRUD para os estudantes cadastrados.

---

## **📋 Funcionalidades**  
- Listagem de estudantes cadastrados  
- Campos de pesquisa para filtragem  
- Botão "Novo" que leva para uma página de cadastramento
- Exclusão de estudantes
- Permite editar cada estudante  

---

## **🚀 Tecnologias Utilizadas**  
Este projeto foi desenvolvido utilizando:  
- [C#](https://learn.microsoft.com/pt-br/dotnet/csharp/)  
- [ASP.NET MVC](https://learn.microsoft.com/pt-br/aspnet/mvc)  
- [SQL Server](https://www.microsoft.com/pt-br/sql-server)  

---

## **⚙️ Pré-requisitos**  
Certifique-se de ter as seguintes ferramentas instaladas:  
- [Visual Studio](https://visualstudio.microsoft.com/pt-br/) (recomendado: versão Community ou superior)  
- [SQL Server](https://www.microsoft.com/pt-br/sql-server)  
- [.NET SDK](https://dotnet.microsoft.com/pt-br/download)  

---

## **📦 Instalação**  

1. Clone o repositório:  
   ```bash
   git clone https://github.com/LuccaTourinho/IEL.git
   ```  

2. Abra o projeto no Visual Studio.  

3. Configure a string de conexão no arquivo `appsettings.json`:  
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=SEU_SERVIDOR;Database=SEU_BANCO;Trusted_Connection=True;TrustServerCertificate=True"
   }
   ```  

4. Execute as migrações do Entity Framework:  
   ```bash
   Add-Migration "Escolhe o nome"
   Update-Database
   ```  

5. Inicie o projeto:  
   - Pressione **F5** no Visual Studio ou execute o comando:  
     ```bash
     dotnet run
     ```  

---

## **📖 Estrutura de Pastas**  
```
ProjetoMVC/
├── Controllers/
├── Models/
├── Views/
├── wwwroot/
├── SQL/
├── Data/
├── appsettings.json
├── Program.cs
```  


## **👨‍💻 Autor**  
**Seu Nome**  
- [GitHub](https://github.com/LuccaTourinho)  
- [LinkedIn](https://www.linkedin.com/in/LuccaTourinho/)  
