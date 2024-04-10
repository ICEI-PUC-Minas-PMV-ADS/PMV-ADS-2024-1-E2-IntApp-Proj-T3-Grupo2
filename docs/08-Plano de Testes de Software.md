# Plano de Testes de Software

<span style="color:red">Pré-requisitos: <a href="2-Especificação do Projeto.md"> Especificação do Projeto</a></span>, <a href="3-Projeto de Interface.md"> Projeto de Interface</a>

Apresente os cenários de testes utilizados na realização dos testes da sua aplicação. Escolha cenários de testes que demonstrem os requisitos sendo satisfeitos.

Não deixe de enumerar os casos de teste de forma sequencial e de garantir que o(s) requisito(s) associado(s) a cada um deles está(ão) correto(s) - de acordo com o que foi definido na seção "2 - Especificação do Projeto". 

Por exemplo:
 
| **Caso de Teste** 	| CT-01 – Cadastro de Instituição (Pessoa jurídica) 	|
|	Requisito Associado 	| RF-001 - A aplicação deve apresentar, na página principal, a funcionalidade de cadastro de instituições para que essas consigam criar e gerenciar seu perfil. |
| Objetivo do Teste 	| Verificar se o usuário consegue se cadastrar na aplicação. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site https://https://padrin.ly//src/index.html<br> - Clicar em "Criar conta" <br> - Preencher os campos obrigatórios (e-mail, nome da instituição, sobrenome, celular, CNPJ, senha, confirmação de senha) <br> - Aceitar os termos de uso <br> - Clicar em "Registrar" |
|Critério de Êxito | - O cadastro foi realizado com sucesso. |
|  	|  	|
| **Caso de Teste** 	| CT-02 – Cadastro de Alunos (Pessoa física)	|
|Requisito Associado | RF-002	- O usuário Instituição cadastrará seus alunos (pessoa fisica) |
| Objetivo do Teste 	| Verificar se após logar, a instituição consegue realizar o cadastro de seus alunos. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site https://https://padrin.ly//src/index.html<br> - Clicar no botão "Entrar" <br> - Preencher o campo de e-mail <br> - Preencher o campo da senha <br> - Clicar em "Login" <br> - Clicar em "Registro de alunos" <br> Preencher os campos obrigatórios (e-mail, nome, sobrenome e CPF) <br> - Aceitar os termos de uso <br> - Clicar em "Registrar aluno" |
|Critério de Êxito | - O cadastro foi efetuado com sucesso. | 
| **Caso de Teste** 	| CT-03 – Cadastro de Apadrinhadores (Pessoa física) 	|
|:---:	|:---:	|
|	Requisito Associado 	| RF-003 - O usuário doador/apadrinhador (pessoa fisica) se cadastrará no site. |
| Objetivo do Teste 	| Verificar se apdrinhadores conseguem efetuar o cadastro no site. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site https://https://padrin.ly//src/index.html<br> - Clicar em "Criar conta" <br> - Preencher os campos obrigatórios (e-mail, nome da instituição, sobrenome, celular, CPF, senha, confirmação de senha) <br> - Aceitar os termos de uso <br> - Clicar em "Registrar" |
|Critério de Êxito | - O cadastro foi efetuado com sucesso. |
|  	|  	|
| **Caso de Teste** 	| CT-04 – Efetuar Login	|
|Requisito Associado | RF-004	- O usuário irá efetuar login no site. |
| Objetivo do Teste 	| Verificar se usuários conseguem efetuar login. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site https://https://padrin.ly//src/index.html<br> - Clicar no botão "Entrar" <br> - Preencher o campo de e-mail <br> - Preencher o campo da senha <br> - Clicar em "Login" <br> |
|Critério de Êxito | - O login foi efetuado com sucesso. |
 
> **Links Úteis**:
> - [IBM - Criação e Geração de Planos de Teste](https://www.ibm.com/developerworks/br/local/rational/criacao_geracao_planos_testes_software/index.html)
> - [Práticas e Técnicas de Testes Ágeis](http://assiste.serpro.gov.br/serproagil/Apresenta/slides.pdf)
> -  [Teste de Software: Conceitos e tipos de testes](https://blog.onedaytesting.com.br/teste-de-software/)
> - [Criação e Geração de Planos de Teste de Software](https://www.ibm.com/developerworks/br/local/rational/criacao_geracao_planos_testes_software/index.html)
> - [Ferramentas de Test para Java Script](https://geekflare.com/javascript-unit-testing/)
> - [UX Tools](https://uxdesign.cc/ux-user-research-and-user-testing-tools-2d339d379dc7)
