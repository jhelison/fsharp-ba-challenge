namespace Migrations
open SimpleMigrations

[<Migration(202112212236L, "Create MarketData")>]
type CreateMarketData() =
  inherit Migration()

  override __.Up() =
    base.Execute(@"CREATE TABLE MarketData(
      id INT NOT NULL,
      date timestamp NOT NULL,
      pair TEXT NOT NULL,
      price FLOAT NOT NULL,
      quantity INT NOT NULL
    )")

  override __.Down() =
    base.Execute(@"DROP TABLE MarketData")
