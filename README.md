# Blog validation

This repository showcases how we use Markdown, MarkDig, and GitHub Actions to
validate our blog.

Our blog's directory structure generally looks like this:

    2014
    └───08-Aug
        └───interns-at-microsoft
            interns-at-microsoft.md
            CharlesLovell.png
            ChristianSalgadoPacheco.png
            IanHays.png
            SantiagoFernandezMadero.png
            ShaunArora.png
            ZachMontoya.png

In other words:

* A top level folder per year
* One nested folder per month, with the two-digit month number, a hyphen, and
  the three letter abbreviation for the month.
* One nested folder per post. The folder name should reflect the post's title.
* The post's folder should contain all assets, especially images

## Validation

You can validate locally by running [validate.cmd](validate.cmd).

The validation also happens as part of CI via the [validate.yml](.github/workflows/validate.yml).