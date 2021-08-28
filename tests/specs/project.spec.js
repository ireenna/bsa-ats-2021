const { expect } = require('chai');

const homeUrl = browser.config.baseUrl + 'home'


describe('Projects:', function () {

  beforeEach(async function () {
    await browser.setWindowSize(1440, 960);
    await browser.url('/login');

    const emailField = await $("input#mat-input-0");
    const passwordField = await $('input[type=password]');
    const signInButton = await $('button');

    await emailField.waitForDisplayed({ timeout: 5000 });
    await emailField.setValue(`hrlead@gmail.com`);

    await passwordField.waitForDisplayed({ timeout: 5000 });
    await passwordField.setValue('hrlead');

    await signInButton.waitForDisplayed({ timeout: 5000 });
    await signInButton.click();

    await browser.waitUntil(
      async function () {
        const url = await browser.getUrl();
        return url === homeUrl;
      },
      { timeout: 5000 },
    );

  }); 

  afterEach(async function () {
    await browser.reloadSession();
  });

  it('should be able to create project', async function () {

    await browser.url('/projects');

    const buttons = await $$("button");
    const newProjectButton = buttons[6];
    await newProjectButton.waitForDisplayed({ timeout: 5000 });
    await newProjectButton.click();


    const inputs = await $$("input")
    const otherInput = inputs[4];
    
    
    console.log(`Shepel=`, inputs.length);
    console.log(`Shepel=`, await otherInput.getHTML());

    const companyName = `Kodak${Math.random()}`.substring(0,14);

    const nameInput = await $("input#mat-input-2");
    await nameInput.waitForDisplayed({ timeout: 5000 });
    await nameInput.setValue(companyName);

    const logoInput = await $("input#mat-input-3");
    await logoInput.waitForDisplayed({ timeout: 5000 });
    await logoInput.setValue(`https://png.pngtree.com/element_pic/00/16/07/06577d261edb9ec.jpg`);


    const descriptionInput = await $("textarea#mat-input-4");
    await descriptionInput.waitForDisplayed({ timeout: 5000 });
    await descriptionInput.setValue(`Sample description`);

    const teamInfoInput = await $("textarea#mat-input-5");
    await teamInfoInput.waitForDisplayed({ timeout: 5000 });
    await teamInfoInput.setValue(`Sample Team Info`);

    const websiteLinkInput = await $("input#mat-input-6");
    await websiteLinkInput.waitForDisplayed({ timeout: 5000 });
    await websiteLinkInput.setValue(`https://kodak.com`);

    const newProjectbuttons = await $$("button");
    const publishButton = newProjectbuttons[9];
    await publishButton.waitForDisplayed({ timeout: 5000 });
    await publishButton.click();

    const searchInput = await $("input#mat-input-0");
    await searchInput.waitForDisplayed({ timeout: 5000 });
    await searchInput.setValue(companyName);

    const companyNameCell = await $('td.cdk-column-name');
    await companyNameCell.waitForDisplayed({ timeout: 5000 });
    const actualCompanyName = await companyNameCell.getText();

    expect(companyName).to.be.eql(actualCompanyName);

  });

});