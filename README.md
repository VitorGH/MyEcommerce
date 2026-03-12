# 🛒 MyEcommerce

Uma aplicação de e-commerce completa desenvolvida com **ASP.NET Core MVC** e **.NET 10**, com autenticação de usuários, carrinho de compras baseado em sessão, painel administrativo e integração com **Stripe** para processamento de pagamentos.

---

## 📋 Índice

- [Funcionalidades](#-funcionalidades)
- [Tecnologias](#-tecnologias)
- [Arquitetura do Projeto](#-arquitetura-do-projeto)
- [Pré-requisitos](#-pré-requisitos)
- [Instalação e Configuração](#-instalação-e-configuração)
- [Configuração do Stripe](#-configuração-do-stripe)
- [Executando o Projeto](#-executando-o-projeto)
- [Estrutura de Pastas](#-estrutura-de-pastas)
- [Rotas Principais](#-rotas-principais)
- [Modelos de Dados](#-modelos-de-dados)
- [Autenticação e Autorização](#-autenticação-e-autorização)
- [Carrinho de Compras](#-carrinho-de-compras)
- [Checkout e Pagamentos](#-checkout-e-pagamentos)
- [Painel Administrativo](#-painel-administrativo)
- [Contribuindo](#-contribuindo)
- [Licença](#-licença)

---

## ✨ Funcionalidades

- **Catálogo de Produtos** — Listagem de produtos na página inicial com nome, descrição, preço e estoque.
- **Carrinho de Compras** — Adicionar produtos ao carrinho, visualizar itens, quantidades e subtotais.
- **Cálculo de Frete** — Consulta de CEP via API [ViaCEP](https://viacep.com.br/) para cálculo de frete.
- **Checkout com Stripe** — Integração com Stripe Checkout para processamento seguro de pagamentos com cartão de crédito.
- **Autenticação de Usuários** — Registro, login e gerenciamento de conta via ASP.NET Core Identity.
- **Painel Administrativo** — CRUD completo de produtos restrito a usuários com role `Admin`.
- **Notificações** — Feedback visual ao adicionar itens ao carrinho via `TempData`.
- **Sessão persistente** — Carrinho armazenado em sessão do servidor com timeout de 30 minutos.

---

## 🛠 Tecnologias

| Tecnologia | Versão |
|---|---|
| .NET | 10.0 |
| ASP.NET Core MVC | 10.0 |
| ASP.NET Core Identity | 10.0 |
| Entity Framework Core | 10.0.2 |
| SQLite | — |
| Stripe.net | 50.3.0 |
| Bootstrap | 5.x |
| Razor Pages | (Identity UI) |

---

## 🏗 Arquitetura do Projeto

O projeto segue o padrão **MVC (Model-View-Controller)** com as seguintes camadas:

```
Controllers/        → Lógica das requisições HTTP (Home, Cart, Checkout, Products)
Models/             → Entidades de domínio (Product, CartItem, ErrorViewModel)
Views/              → Razor Views para renderização de UI
Services/           → Serviços de negócio (StripeService)
Data/               → DbContext e Migrations do Entity Framework Core
```

---

## 📦 Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Uma conta no [Stripe](https://stripe.com/) (para obter as chaves de API)
- Editor de código (recomendado: Visual Studio 2022+ ou VS Code)

---

## 🚀 Instalação e Configuração

### 1. Clonar o repositório

```bash
git clone https://github.com/VitorGH/MyEcommerce.git
cd MyEcommerce
```

### 2. Restaurar dependências

```bash
dotnet restore
```

### 3. Aplicar as migrations do banco de dados

O projeto utiliza **SQLite** com o arquivo `app.db`. Para criar/atualizar o banco:

```bash
dotnet ef database update
```

### 4. Configurar as chaves do Stripe

Edite o arquivo `appsettings.json` ou utilize **User Secrets** para configurar suas chaves do Stripe:

#### Via `appsettings.json`:

```json
{
  "Stripe": {
    "SecretKey": "sk_test_sua_chave_secreta",
    "PublicKey": "pk_test_sua_chave_publica"
  }
}
```

#### Via User Secrets (recomendado para desenvolvimento):

```bash
dotnet user-secrets set "Stripe:SecretKey" "sk_test_sua_chave_secreta"
dotnet user-secrets set "Stripe:PublicKey" "pk_test_sua_chave_publica"
```

---

## 💳 Configuração do Stripe

1. Crie uma conta em [dashboard.stripe.com](https://dashboard.stripe.com/).
2. Acesse **Developers > API Keys** para obter suas chaves de teste.
3. Configure as chaves conforme descrito na seção anterior.
4. Para testes, utilize o cartão: `4242 4242 4242 4242` com qualquer data futura e CVC.

---

## ▶ Executando o Projeto

```bash
dotnet run
```

A aplicação estará disponível em:

- `https://localhost:5001` (HTTPS)
- `http://localhost:5000` (HTTP)

### Conta Administrador Padrão

Na primeira execução, uma conta de administrador é criada automaticamente:

| Campo | Valor |
|---|---|
| Email | `admin@admin.com` |
| Senha | `Admin@123456` |

---

## 📁 Estrutura de Pastas

```
MyEcommerce/
├── Controllers/
│   ├── HomeController.cs          # Página inicial + AddToCart
│   ├── CartController.cs          # Visualização do carrinho
│   ├── CheckoutController.cs      # Criação de sessão Stripe + página de sucesso
│   └── ProductsController.cs      # CRUD de produtos (Admin)
├── Data/
│   ├── ApplicationDbContext.cs     # DbContext com Identity + Products
│   └── Migrations/                # Migrations do EF Core
├── Models/
│   ├── Product.cs                 # Entidade Produto
│   ├── CartItem.cs                # Modelo do item do carrinho
│   └── ErrorViewModel.cs          # Modelo de erro
├── Services/
│   ├── IStripeService.cs          # Interface do serviço Stripe
│   └── StripeService.cs           # Implementação da integração Stripe
├── Views/
│   ├── Home/
│   │   └── Index.cshtml           # Catálogo de produtos
│   ├── Cart/
│   │   └── Index.cshtml           # Página do carrinho
│   ├── Products/                  # Views CRUD de produtos (Admin)
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   ├── Details.cshtml
│   │   └── Delete.cshtml
│   └── Shared/
│       ├── _Layout.cshtml         # Layout principal
│       └── _LoginPartial.cshtml   # Barra de navegação com login
├── wwwroot/                       # Arquivos estáticos (CSS, JS, libs)
├── appsettings.json               # Configurações da aplicação
├── Program.cs                     # Configuração e inicialização
├── MyEcommerce.csproj             # Arquivo de projeto
└── app.db                         # Banco de dados SQLite
```

---

## 🔀 Rotas Principais

| Método | Rota | Descrição | Autenticação |
|---|---|---|---|
| GET | `/` | Catálogo de produtos | Pública |
| POST | `/Home/AddToCart` | Adicionar produto ao carrinho | Requer login |
| GET | `/Cart` | Visualizar carrinho | Pública |
| POST | `/Checkout/CreateSession` | Iniciar pagamento via Stripe | Pública |
| GET | `/Checkout/Success` | Página de sucesso pós-pagamento | Pública |
| GET | `/Admin/Products` | Listar produtos (admin) | Admin |
| GET | `/Admin/Products/Create` | Formulário de criação | Admin |
| POST | `/Admin/Products/Create` | Criar produto | Admin |
| GET | `/Admin/Products/Edit/{id}` | Formulário de edição | Admin |
| POST | `/Admin/Products/Edit/{id}` | Editar produto | Admin |
| GET | `/Admin/Products/Details/{id}` | Detalhes do produto | Admin |
| GET | `/Admin/Products/Delete/{id}` | Confirmação de exclusão | Admin |
| POST | `/Admin/Products/Delete/{id}` | Excluir produto | Admin |

---

## 📐 Modelos de Dados

### Product

| Campo | Tipo | Descrição |
|---|---|---|
| `Id` | `int` | Chave primária |
| `Name` | `string` | Nome do produto |
| `Description` | `string` | Descrição do produto |
| `Price` | `decimal` | Preço unitário (R$) |
| `Amount` | `int` | Quantidade em estoque |

### CartItem (sessão)

| Campo | Tipo | Descrição |
|---|---|---|
| `ProductId` | `int` | ID do produto |
| `ProductName` | `string` | Nome do produto |
| `Price` | `decimal` | Preço unitário |
| `Quantity` | `int` | Quantidade no carrinho |

---

## 🔐 Autenticação e Autorização

O projeto utiliza **ASP.NET Core Identity** com as seguintes configurações:

- **Confirmação de email** obrigatória no registro (`RequireConfirmedAccount = true`).
- **Roles** suportadas: `Admin`.
- A role `Admin` é criada automaticamente na inicialização.
- Um usuário administrador padrão é provisionado no seed (`admin@admin.com`).
- O painel de produtos em `/Admin/Products` é protegido com `[Authorize(Roles = "Admin")]`.
- Páginas de Identity (Login, Register, Manage) são fornecidas pelo pacote `Microsoft.AspNetCore.Identity.UI`.

---

## 🛒 Carrinho de Compras

O carrinho é implementado utilizando **sessão do servidor**:

- Os itens são serializados como JSON e armazenados via `HttpContext.Session`.
- Timeout da sessão: **30 minutos**.
- Ao adicionar um produto já existente no carrinho, a **quantidade é incrementada**.
- Usuários não autenticados são redirecionados para a página de login ao tentar adicionar itens.
- A página do carrinho exibe uma tabela com nome, preço, quantidade, subtotal e total geral.
- Inclui consulta de CEP via API **ViaCEP** para cálculo de frete.

---

## 💳 Checkout e Pagamentos

A integração com o **Stripe** é feita via `StripeService`:

1. O usuário clica em **"Finalizar Compra"** na página do carrinho.
2. O `CheckoutController` cria uma sessão de checkout no Stripe com os itens do carrinho.
3. O usuário é redirecionado para a página de pagamento hospedada pelo Stripe.
4. Moeda configurada: **BRL (Real Brasileiro)**.
5. Após o pagamento bem-sucedido, o carrinho é limpo e o usuário vê a página de sucesso.

---

## 🔧 Painel Administrativo

Acessível em `/Admin/Products`, o painel permite:

- **Listar** todos os produtos cadastrados.
- **Criar** novos produtos com nome, descrição, preço e quantidade.
- **Editar** informações de produtos existentes.
- **Visualizar** detalhes de um produto específico.
- **Excluir** produtos do catálogo.

> ⚠️ Apenas usuários com a role **Admin** podem acessar o painel.

---

## 🤝 Contribuindo

1. Faça um fork do projeto.
2. Crie uma branch para sua feature: `git checkout -b minha-feature`.
3. Faça commit das alterações: `git commit -m 'Adiciona minha feature'`.
4. Faça push para a branch: `git push origin minha-feature`.
5. Abra um Pull Request.

---

## 📄 Licença

Este projeto está sob a licença MIT. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.
