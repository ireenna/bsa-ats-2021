const { expect } = require('chai');

const homeUrl = browser.config.baseUrl + 'home'


describe('Login:', function () {
  beforeEach(async function () {
    await browser.setWindowSize(1440, 960);
    await browser.url('/login');
  });

  afterEach(async function () {
    await browser.reloadSession();
  });

  xit('should be able to login', async function () {
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

    const url = await browser.getUrl();
    expect(url).to.be.eql(homeUrl);

  });

});