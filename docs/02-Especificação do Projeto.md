# Especificações do Projeto

<span style="color:red">Pré-requisitos: <a href="1-Documentação de Contexto.md"> Documentação de Contexto</a></span>

Definição do problema e ideia de solução a partir da perspectiva do usuário. É composta pela definição do  diagrama de personas, histórias de usuários, requisitos funcionais e não funcionais além das restrições do projeto.

Apresente uma visão geral do que será abordado nesta parte do documento, enumerando as técnicas e/ou ferramentas utilizadas para realizar a especificações do projeto

## Personas

Pedro Paulo tem 26 anos, é arquiteto recém-formado e autônomo. Pensa em se desenvolver profissionalmente através de um mestrado fora do país, pois adora viajar, é solteiro e sempre quis fazer um intercâmbio. Está buscando uma agência que o ajude a encontrar universidades na Europa que aceitem alunos estrangeiros.

Enumere e detalhe as personas da sua solução. Para tanto, baseie-se tanto nos documentos disponibilizados na disciplina e/ou nos seguintes links:

> **Links Úteis**:
> - [Rock Content](https://rockcontent.com/blog/personas/)
> - [Hotmart](https://blog.hotmart.com/pt-br/como-criar-persona-negocio/)
> - [O que é persona?](https://resultadosdigitais.com.br/blog/persona-o-que-e/)
> - [Persona x Público-alvo](https://flammo.com.br/blog/persona-e-publico-alvo-qual-a-diferenca/)
> - [Mapa de Empatia](https://resultadosdigitais.com.br/blog/mapa-da-empatia/)
> - [Mapa de Stalkeholders](https://www.racecomunicacao.com.br/blog/como-fazer-o-mapeamento-de-stakeholders/)
>
Lembre-se que você deve ser enumerar e descrever precisamente e personalizada todos os clientes ideais que sua solução almeja.

## Histórias de Usuários

Com base na análise das personas forma identificadas as seguintes histórias de usuários:

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
| Pais e Alunos      |  <br />Cadastro do usuário com verificação documental.<br /> Interação entre os usuários da plataforma. <br /> Ter acesso as doações que foram concedidas.|<br />Para que seja permitido o acesso a plataforma com segurança.<br /> Para que haja comunicação entre as partes.<br /> Para que haja acesso a informação do beneficio concedido.|
|Empreendedor Social | <br /> Cadastro do usuário podendo ser feito para pessoa física ou jurídica. <br /> Interação entre os usuários da plataforma.<br />  Ter acesso aos beneficiários que iram receber a doação.  |<br /> Para que seja permitido o acesso a plataforma. <br /> Para que haja comunicação entre as partes.  <br />  Para que as doações sejam ofertadas. |
|Administrador da instituição|<br /> Cadastro do usuário. <br /> Gerenciar dados do aluno. <br /> Interação entre os usuários da plataforma. <br /> Ter a total visualização de todos os cadastros e movimentações da plataforma realizado pelos usuários.  | <br /> Para que seja permitido o acesso a plataforma. <br /> Para que haja comunicação entre as partes.  <br /> Para que seja monitorado a frequência do aluno nas aulas tanto como notas doações e participação. <br /> Para que haja comunicação entre as partes. <br /> Para manter o controle do sistema. |
> **Links Úteis**:
> - [Histórias de usuários com exemplos e template](https://www.atlassian.com/br/agile/project-management/user-stories)
> - [Como escrever boas histórias de usuário (User Stories)](https://medium.com/vertice/como-escrever-boas-users-stories-hist%C3%B3rias-de-usu%C3%A1rios-b29c75043fac)
> - [User Stories: requisitos que humanos entendem](https://www.luiztools.com.br/post/user-stories-descricao-de-requisitos-que-humanos-entendem/)
> - [Histórias de Usuários: mais exemplos](https://www.reqview.com/doc/user-stories-example.html)
> - [9 Common User Story Mistakes](https://airfocus.com/blog/user-story-mistakes/)

## Requisitos

As tabelas que se seguem apresentam os requisitos funcionais e não funcionais que detalham o escopo do projeto.

### Requisitos Funcionais

|ID    | Descrição do Requisito  | Prioridade |
|------|-----------------------------------------|----|
|RF-001| O usuário Administrador cadastrará Instituição (pessoa juridica) | ALTA | 
|RF-002| O usuário Instituição cadastrará seus alunos (pessoa fisica)  | ALTA |
|RF-003| O usuário doador/apadrinhador (pessoa fisica) se cadastrará no site  | ALTA |
|RF-004| O usuário irá efetuar login no site  | ALTA |
|RF-005| As instituições cadastradas devem ter a capacidade de gerenciar alunos vinculados a elas  | ALTA |
|RF-006| O administrador do site deve ter a capacidade de gerenciar as instituições vinculadas | ALTA |
|RF-007| O usuário aluno ira publicar textos e/ou imagens em seu mural   | MÉDIA |
|RF-007| O usuário aluno poderá fixar postagens em seu mural  | BAIXA |
|RF-008| Os usuários irão interagir uns com os outros através do envio de mensagens   | BAIXA |
|RF-009| Os usuários devem aceitar termos de compromisso e responsabilidades para interagir no sistema   | ALTA |


### Requisitos não Funcionais

|ID     | Descrição do Requisito  |Prioridade |
|-------|-------------------------|----|
|RNF-001| Os usuários "pessoa" devem ser anônimos por padrão até que assinem os termos de responsabilidade e possam interagir com o apadrinhado. | ALTA | 
|RNF-002| Garantir que o sistema seja rápido e responsivo, mesmo durante períodos de alto tráfego ou carga intensiva de dados. | ALTA | 
|RNF-003| Certificar-se de que o sistema funcione corretamente em uma variedade de navegadores web e dispositivos, incluindo desktops, tablets e smartphones. | MÉDIA | 
|RNF-004| Projetar o sistema de forma modular e fácil de manter, permitindo atualizações e correções de bugs sem interrupções significativas no serviço. | BAIXA | 


## Diagrama de Casos de Uso

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2/blob/main/docs/img/Caso%20de%20Uso.drawio.png)
